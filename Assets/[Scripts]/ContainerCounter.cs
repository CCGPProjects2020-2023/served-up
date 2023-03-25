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

    public GameObject placePOSTransform;

    public GameObject itemPrefab; 

    Transform[] transforms;



    private void Start()
    {
        itemPos = transform.GetChild(0).gameObject;

        placePOS = transform.gameObject;

        //placePOSTransform = placePOS.transform.gameObject;

    }


    private void Update()
    {
       
       
        transforms = placePOS.GetComponentsInChildren<Transform>();


        foreach (Transform child in transforms)
        {
            if (child != null)
            {
                Instantiate(itemPrefab, placePOS.transform);
                //Destroy(child.gameObject);
            }
            else
            {
                Instantiate(itemPrefab, placePOS.transform);

                itemPrefab = item;
            }
        } 




        /*if (placePOSCHILD == null)
        {
            Instantiate(itemPrefab, placePOS.transform);

            itemPrefab = item;
        }
        else
        {
            // idk
        } */

       /* placePOSCHILD = itemPos.transform.GetChild(0).gameObject;

        if (placePOSCHILD != null)
        {
           
            
        }
        else
        {
            Instantiate(item, placePOS.transform);
        } */
    }


    public void FindItem()
    {
       // item = Instantiate(item, itemPos.transform);
    }
}
