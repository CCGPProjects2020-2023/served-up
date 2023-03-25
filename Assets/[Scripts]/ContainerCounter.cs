using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : Placeable
{
    // Start is called before the first frame update

    //  [SerializeField] private ItemSO itemSO;

    //private ItemSOHolder itemSOHolder;

    public GameObject placePOS;

    public GameObject placePOSCHILD;



    private void Start()
    {
        itemPos = transform.GetChild(0).gameObject;

    }


    private void Update()
    {
        placePOSCHILD = itemPos.transform.GetChild(0).gameObject;

        if (placePOSCHILD != null)
        {
           
        }
        else
        {
            Instantiate(item, placePOS.transform);
        }
    }


    public void FindItem()
    {
       // item = Instantiate(item, itemPos.transform);
    }
}
