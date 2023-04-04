using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Timers;

public class Table : Placeable
{
    public GameObject emptyCup;
    public GameObject customerPos;
    public SpriteRenderer orderImage;
    public GameObject customer;
    public ItemSO order;
    public GameObject tempItem;
    private bool canTakeOrder;

    public float currentTimer;
    public float timer;

    IEnumerator Timer(float time, Action actionToBeExecuted)
    {
        currentTimer = time;
        timer = currentTimer;
        while (true)
        {
            timer = time;
            time -= Time.deltaTime;
            if (time < 0)
            {
                actionToBeExecuted();
                yield break;
            }
            yield return null;
        }
    }

    private void EatingComplete()
    {
        Debug.Log("Eating Complete");
        OrderComplete();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void ThinkingComplete()
    {
        canTakeOrder = true;
        StartCoroutine(Timer(GameManager.Instance.serviceTime, GameOver));
    }

  

    private void Update()
    {
        if(item && item.GetComponent<ItemSOHolder>().itemSO == order)
        {
            tempItem = item;
            item = null;
            StopAllCoroutines();
            StartCoroutine(Timer(GameManager.Instance.eatingTime, EatingComplete));
        }
    }
    private void OrderComplete()
    {
        Destroy(tempItem);
        tempItem = null;
        order = null;
        GameObject newItem = Instantiate(emptyCup, Vector3.zero, emptyCup.transform.rotation);
        newItem.transform.SetParent(itemPos.transform);
        item = newItem;
        item.transform.localPosition = Vector3.zero;
        customer.GetComponent<CustomerAnimation>().LeaveTable();
        customer = null;
        orderImage.sprite = null;
        canTakeOrder = false;
        Events.onOrderCompleted.Invoke();
    }

    public void TakeOrder()
    {
        if (customer)
        {
            if(canTakeOrder)
            {
                StopAllCoroutines();
                StartCoroutine(Timer(GameManager.Instance.deliveryTime, GameOver));
                order = OrderManager.Instance.GenerateOrder();
                orderImage.sprite = order.icon;
                Debug.Log(order.name);
            }
        }
        
    }
    private void OnCustomerReachedTable(Table obj)
    {
        if(obj == this)
        {
            StartCoroutine(Timer(GameManager.Instance.thinkingTime, ThinkingComplete));
        }
    }

    private void OnEnable()
    {
        Events.onCustomerReachedTable.AddListener(OnCustomerReachedTable);
        Events.onObjectSelectedChanged.AddListener(OnObjectSelectedChanged);
    }

    private void OnDisable()
    {
        Events.onCustomerReachedTable.RemoveListener(OnCustomerReachedTable);
        Events.onObjectSelectedChanged.RemoveListener(OnObjectSelectedChanged);
    }

}
