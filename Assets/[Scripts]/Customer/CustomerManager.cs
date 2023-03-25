using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        Events.onOrderCompleted.AddListener(OnOrderCompleted);
    }
    private void OnDisable()
    {
        Events.onOrderCompleted.RemoveListener(OnOrderCompleted);
    }

    // Update is called once per frame
    void Update()
    {
        //DELETE WHEN WE ADD CUSTOMER SPAWNING!!!!!!!!!!!!!!!!!!!!!!
        if (Input.GetKeyDown(KeyCode.G))
        {
            CreateCustomer();
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
                customer.transform.position = table.transform.position;
                return;
            }
        }
        foreach (QueuePosition queuePosition in queuePositions)
        {
            if (queuePosition.customer == null)
            {
                queuePosition.customer = customer;
                customer.transform.position = queuePosition.gameObject.transform.position;
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
                            currentCustomer.transform.position = table.transform.position;
                            break;
                        }

                    }
                }
                else
                {
                    queuePositions[i - 1].customer = currentCustomer;
                    currentCustomer.transform.position = queuePositions[i - 1].gameObject.transform.position;
                }
            }
            
        }
            
    }
}
