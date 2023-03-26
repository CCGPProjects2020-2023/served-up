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

        itemPrefab = item;

        Debug.Log(item.transform.childCount + "  by " + itemPos.name);

    }


    private void Update()
    {
        if (itemPos.transform.childCount == 0)
        {
            Instantiate(item, itemPos.transform);
        }
    }
}
