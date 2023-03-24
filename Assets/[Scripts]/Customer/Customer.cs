using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Customer
{
    [SerializeField] private float interarrivalTime;

    public Customer(float interarrivalTime)
    {
        //this.customerNum = customerNum;
        this.interarrivalTime = interarrivalTime;
    }

    public float GetInterarrivalTime()
    {
        return this.interarrivalTime;
    }
}
