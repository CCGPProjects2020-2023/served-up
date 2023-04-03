using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerManager : MonoBehaviour
{
    public List<QueuePosition> queuePositions;
    public List<Table> tables;
    public GameObject customerPrefab;
    public List<GameObject> customersInScene;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnDayStarted()
    {
        for (int i = 0; i < GameManager.Instance.currentCustomers; i++)
        {
            CreateCustomer();
        }
        StartCoroutine(CheckCustomerAmount());
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

    IEnumerator CheckCustomerAmount()
    {
        while(true)
        {
            if (GameObject.FindGameObjectsWithTag("Customer").Length <= 0)
            {
                Events.onDayCompleted.Invoke();
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
        
    }


    public void CreateCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, Vector3.zero, Quaternion.identity);
        customersInScene.Add(customer);
        CheckPositions(customer);
    }
    private void CheckPositions(GameObject customer)
    {
        foreach (Table table in tables)
        {
            if (table.customer == null)
            {
                table.customer = customer;
                customer.GetComponent<CustomerAnimation>().table = table;
                customer.GetComponent<CustomerAnimation>().isInQueue = false;
                customer.GetComponent<NavMeshAgent>().SetDestination(table.customerPos.transform.position);
                //customer.transform.position = table.customerPos.transform.position;
                //customer.transform.rotation = table.customerPos.transform.rotation;
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
        GameObject currentCustomer;
        for(int i= 0; i < queuePositions.Count; i++)
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
}
