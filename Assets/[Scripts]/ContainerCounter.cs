using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ItemSO itemSO;

    private ItemSOHolder itemSOHolder;
    private PlayerController playerController;

    [SerializeField] private GameObject itemPOS;
    [SerializeField] public GameObject item;
    [SerializeField] public GameObject heldItem;
    Placeable placeable;




    private void Start()
    {
        Placeable placeable = GetComponent<Placeable>();

      /*  playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerController.Pickup();

        placeable.itemPos = itemPOS;

        itemPOS = transform.GetChild(0).gameObject; */        
    }


    public void FindItem()
    {
        placeable.item = Instantiate(item, itemPOS.transform);
    }
}
