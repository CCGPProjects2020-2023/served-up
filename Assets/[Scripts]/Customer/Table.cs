using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Table : Placeable
{
    public GameObject emptyCup;
    public GameObject customerPos;
    public SpriteRenderer orderImage;
    public GameObject customer;
    public ItemSO order;
    private void Update()
    {
        if(item && item.GetComponent<ItemSOHolder>().itemSO == order)
        {
            OrderComplete();
        }
    }
    private void OrderComplete()
    {
        Destroy(item);
        item = null;
        order = null;
        GameObject newItem = Instantiate(emptyCup, new Vector3(0, 0, 0), emptyCup.transform.rotation);
        newItem.transform.SetParent(itemPos.transform);
        item = newItem;
        item.transform.localPosition = Vector3.zero;
        customer.GetComponent<CustomerAnimation>().LeaveTable();
        customer = null;
        orderImage.sprite = null;
        Events.onOrderCompleted.Invoke();
    }

    public void TakeOrder()
    {
        if (customer)
        {
            if(customer.GetComponent<CustomerAnimation>().isCustomerAtTable)
            {
                order = OrderManager.Instance.GenerateOrder();
                orderImage.sprite = order.icon;
                Debug.Log(order.name);
            }
        }
        
    }


}
