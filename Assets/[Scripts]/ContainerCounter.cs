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

    PlayerController player;



    Transform[] transforms;



    private void Start()
    {
        itemPos = transform.GetChild(0).gameObject;

        PlayerController player = GetComponent<PlayerController>();
    }


    private void Update()
    {
       /* if (itemPos.transform.childCount == 0)
        {
            Instantiate(item, itemPos.transform);
        } */
    }

    public void GetItem()
    {
        if (player.heldItemPos == null)
        {
            Instantiate(item, itemPos.transform);
        }
    }

}
