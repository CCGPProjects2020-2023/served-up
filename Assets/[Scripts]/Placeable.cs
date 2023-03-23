using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public GameObject itemPos;
    public GameObject item;
    //should store item on it as well

    private void Start()
    {
        itemPos = transform.GetChild(0).gameObject;
    }
}
