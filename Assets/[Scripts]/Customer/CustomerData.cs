using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class CustomerData
{
    [SerializeField] private float interarrivalTime;

    public CustomerData(float interarrivalTime)
    {
        //this.customerNum = customerNum;
        this.interarrivalTime = interarrivalTime;
    }

    public float GetInterarrivalTime()
    {
        return this.interarrivalTime;
    }
}
