using System;
using System.Collections;
using UnityEngine;

public class Table : Placeable
{
    public GameObject emptyCup;
    public GameObject customerPos;
    public SpriteRenderer orderImage;
    public GameObject customer;
    public ItemSO order;
    public GameObject tempItem;
    private bool canTakeOrder;
    public TableState currentState;
    public float currentTimer;
    public float timer;
    private void Start()
    {
        currentState = TableState.None;
    }
    private void Update()
    {
        if (item && item.GetComponent<ItemSOHolder>().itemSO == order)
        {
            tempItem = item;
            tempItem.layer = 0;
            item = null;
            StopAllCoroutines();
            currentState = TableState.Eating;
            customer.GetComponent<CustomerAnimation>().StartDrinkingAnim();
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
        timer = 0;
        currentTimer = 0;
        currentState = TableState.None;
        Events.onTableStateChanged.Invoke(this, currentState);
        Events.onOrderCompleted.Invoke();
    }

    public void TakeOrder()
    {
        if (customer)
        {
            if (canTakeOrder)
            {
                StopAllCoroutines();
                currentState = TableState.Delivery;
                StartCoroutine(Timer(GameManager.Instance.deliveryTime, GameOver));
                order = OrderManager.Instance.GenerateOrder();
                orderImage.sprite = order.icon;
                Debug.Log(order.name);
            }
        }

    }
    private void OnCustomerReachedTable(Table obj)
    {
        if (obj == this)
        {
            currentState = TableState.Thinking;
            StartCoroutine(Timer(GameManager.Instance.thinkingTime, ThinkingComplete));
        }
    }
    public IEnumerator Timer(float time, Action actionToBeExecuted)
    {
        currentTimer = time;
        timer = currentTimer;
        Events.onTableStateChanged.Invoke(this, currentState);
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

    public void DrinkingComplete()
    {
        OrderComplete();
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        Events.onGameOver.Invoke();
    }

    private void ThinkingComplete()
    {
        canTakeOrder = true;
        currentState = TableState.Service;
        StartCoroutine(Timer(GameManager.Instance.serviceTime, GameOver));
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
public enum TableState
{
    None,
    Thinking,
    Service,
    Delivery,
    Eating
}
