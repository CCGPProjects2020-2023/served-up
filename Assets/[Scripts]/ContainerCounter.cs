using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ItemSO itemSO;

    private ItemSOHolder itemSOHolder;
    private PlayerController playerController;

    [SerializeField] private GameObject placePOS;

    private void Start()
    {
        Placeable placeable = GetComponent<Placeable>();

        Transform [] children = GetComponentsInChildren<Transform>();



        if (placePOS.transform.childCount == 0)
        {
            placeable.item = Instantiate(itemSO.prefab, placePOS.transform);
        }        
    }


    public void Pickup()
    {
       
    }




}
