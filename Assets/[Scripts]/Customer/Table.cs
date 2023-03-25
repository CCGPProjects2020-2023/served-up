using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Placeable
{
    public bool isEmpty;
    public GameObject customer;
    public ItemSO order;
    public GameObject emptyCup;
    // Start is called before the first frame update
    void Start()
    {
        itemPos = transform.GetChild(0).gameObject;
        selectedCounterVisual = transform.GetChild(1).gameObject;
        isEmpty = true;   
    }
    private void Update()
    {
        if(item && item.GetComponent<ItemSOHolder>().itemSO == order)
        {
            Destroy(item);
            item = null;
            order = null;
            GameObject newItem = Instantiate(emptyCup, new Vector3(0, 0, 0), emptyCup.transform.rotation);
            newItem.transform.SetParent(itemPos.transform);
            item = newItem;
            item.transform.localPosition = Vector3.zero;
            Debug.Log("order complete");
            Destroy(customer);
            customer = null;
            
            Events.onOrderCompleted.Invoke();
            
        }
    }
    public void TakeOrder()
    {
        if (customer)
        {
            order = OrderManager.Instance.GenerateOrder();
            Debug.Log(order.name);
        }
        
    }


    
}
