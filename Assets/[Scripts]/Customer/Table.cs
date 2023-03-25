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
            Debug.Log("order complete");
        }
    }
    public void TakeOrder()
    {
        order = OrderManager.Instance.GenerateOrder();
        Debug.Log(order.name);
    }


    
}
