using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    public GameObject itemPos;
    public GameObject item;
    public GameObject selectedCounterVisual;
    //should store item on it as well

    private void Start()
    {
        itemPos = transform.GetChild(0).gameObject;
        selectedCounterVisual = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        Events.onObjectSelectedChanged.AddListener(OnObjectSelectedChanged);
    }
    private void OnDisable()
    {
        Events.onObjectSelectedChanged.RemoveListener(OnObjectSelectedChanged);
    }

    public void OnObjectSelectedChanged(GameObject obj)
    {
        if(obj == gameObject)
        {
            selectedCounterVisual.SetActive(true);
            if(item && item.transform.childCount > 0)
            {
                item.transform.GetChild(0).gameObject.SetActive(true);
            }
        } else
        {
            if (item && item.transform.childCount > 0)
            {
                item.transform.GetChild(0).gameObject.SetActive(false);
            }
            selectedCounterVisual.SetActive(false);
        }
    }
}
