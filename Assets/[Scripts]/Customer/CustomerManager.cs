using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public List<QueuePosition> queuePositions;
    public List<Table> tables;
    public GameObject customerPrefab;
    public List<GameObject> customersInScene;
    public List<float> interarrivalTimes;
    public float customersServed;

    [Header("Queue Patience")]
    public float maxWaitTime = 90;
    public float currentQueueTimer;
    public float queueRecoveryTime = 10;

    private void Start()
    {
        currentQueueTimer = maxWaitTime;
        StartCoroutine(Timer(QueuePatienceReached));
    }

    private void Update()
    {
        CalculateMusicTempo();
    }

    private void CalculateMusicTempo()
    {
        //get table timer ratios
        List<float> tableRatios = new List<float>();
        foreach (Table table in tables)
        {
            if (table.currentTimer == 0 || table.currentState == TableState.Thinking || table.currentState == TableState.Eating)
                continue;
            tableRatios.Add(table.timer / table.currentTimer);
        }
        float minTableRatio = 1;
        if (tableRatios.Count > 0)
            minTableRatio = tableRatios.Min();

        float queueRatio = currentQueueTimer / maxWaitTime;

        float minRatio = Math.Min(minTableRatio, queueRatio);

        AudioManager.Instance.SetTempoParameter("tempo", 1 - minRatio);
    }
    private void OnDayStarted()
    {
        customersServed = 0;
        interarrivalTimes = new List<float>();
        interarrivalTimes.Clear();
        for (int i = 0; i < GameManager.Instance.currentCustomers; i++)
        {
            interarrivalTimes.Add(GenerateInterarrivalValue());
        }
        StartCoroutine(SpawnCustomers());
    }

    #region Queue Patience
    public IEnumerator Timer(Action actionToBeExecuted)
    {
        while (true)
        {
            if (AmountOfCustomersInQueue() > 0)
            {
                //each customer in queue makes timer faster multiplies with itself caps at 5x faster
                float queuersFactor = (float)Math.Pow(1.1, AmountOfCustomersInQueue());
                if (queuersFactor > 5)
                    queuersFactor = 5;
                float timeChange = 1 * queuersFactor * Time.deltaTime;
                currentQueueTimer -= timeChange;
                if (currentQueueTimer < 0)
                {
                    actionToBeExecuted();
                    yield break;
                }
            }
            else
            {
                currentQueueTimer = maxWaitTime;
            }
            yield return null;
        }
    }

    public void QueuePatienceReached()
    {
        Events.onGameOver.Invoke();
    }

    public void RecoverQueueTime()
    {
        currentQueueTimer += queueRecoveryTime;
        if (currentQueueTimer > maxWaitTime)
            currentQueueTimer = maxWaitTime;
    }

    public int AmountOfCustomersInQueue()
    {
        int count = 0;
        foreach (QueuePosition queue in queuePositions)
        {
            if (queue.customer)
            {
                count++;
            }
        }

        return count;
    }
    #endregion
    #region Customer Spawning
    IEnumerator CheckCustomerAmount()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Customer").Length <= 0)
            {
                Events.onDayCompleted.Invoke();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }

    }
    IEnumerator SpawnCustomers()
    {
        for (int i = 0; i < GameManager.Instance.currentCustomers; i++)
        {
            yield return new WaitForSeconds(interarrivalTimes[i]);
            CreateCustomer();
        }
        yield return null;
    }
    public void CreateCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, Vector3.zero, Quaternion.identity);
        customersInScene.Add(customer);
        CheckPositions(customer);
    }
    #endregion
    #region Customer Queue Logic
    private void CheckPositions(GameObject customer)
    {
        foreach (Table table in tables)
        {
            if (table.customer == null)
            {
                RecoverQueueTime();
                table.customer = customer;
                customer.GetComponent<CustomerAnimation>().table = table;
                customer.GetComponent<CustomerAnimation>().isInQueue = false;
                customer.GetComponent<NavMeshAgent>().SetDestination(table.customerPos.transform.position);
                return;
            }
        }
        foreach (QueuePosition queuePosition in queuePositions)
        {
            if (queuePosition.customer == null)
            {
                queuePosition.customer = customer;
                customer.GetComponent<CustomerAnimation>().isInQueue = true;
                customer.GetComponent<NavMeshAgent>().SetDestination(queuePosition.transform.position);
                break;
            }
        }
    }
    private void OnOrderCompleted()
    {
        customersServed++;
        CheckCustomersServed();
        GameObject currentCustomer;
        for (int i = 0; i < queuePositions.Count; i++)
        {
            if (queuePositions[i].customer)
            {
                currentCustomer = queuePositions[i].customer;
                queuePositions[i].customer = null;
                if (queuePositions[0] == queuePositions[i])
                {

                    foreach (Table table in tables)
                    {
                        if (table.customer == null)
                        {
                            RecoverQueueTime();
                            table.customer = currentCustomer;
                            currentCustomer.GetComponent<CustomerAnimation>().table = table;
                            currentCustomer.GetComponent<CustomerAnimation>().isInQueue = false;
                            currentCustomer.GetComponent<NavMeshAgent>().SetDestination(table.customerPos.transform.position);
                            break;
                        }

                    }
                }
                else
                {
                    queuePositions[i - 1].customer = currentCustomer;
                    currentCustomer.GetComponent<CustomerAnimation>().isInQueue = true;
                    currentCustomer.GetComponent<NavMeshAgent>().SetDestination(queuePositions[i - 1].gameObject.transform.position);
                }
            }

        }
    }
    #endregion

    private void CheckCustomersServed()
    {
        if (customersServed >= GameManager.Instance.currentCustomers)
        {
            StartCoroutine(CheckCustomerAmount());
        }
    }
    private float GenerateInterarrivalValue()
    {
        float lambda = ((100 + 25 * Mathf.Floor((GameManager.Instance.currentDay - 1) / 3f)) / GameManager.Instance.currentCustomers);
        float maxDeviationValue = 5 - 0.25f * GameManager.Instance.currentCustomers;
        float randomValue = (-Mathf.Log(1 - Random.value) / lambda) * 1000;
        while (randomValue > lambda + maxDeviationValue || randomValue < lambda - maxDeviationValue)
        {
            randomValue = (-Mathf.Log(1 - Random.value) / lambda) * 1000;
        }
        Debug.Log(randomValue);
        return randomValue;
    }

    private void OnEnable()
    {
        Events.onOrderCompleted.AddListener(OnOrderCompleted);
        Events.onDayStarted.AddListener(OnDayStarted);

    }
    private void OnDisable()
    {
        Events.onOrderCompleted.RemoveListener(OnOrderCompleted);
        Events.onDayStarted.RemoveListener(OnDayStarted);
    }
}
