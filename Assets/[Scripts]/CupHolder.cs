using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupHolder : Placeable
{
    public List<GameObject> cupList = new List<GameObject>();
    public int currentCups;
    public int maxCups;
    public GameObject emptyCup;

    private void Start()
    {
        maxCups = 6;
        currentCups = 4;
        UpdateCups();
    }

    private void Update()
    {
        
    }

    public void UpdateCups()
    {
        foreach(GameObject cup in cupList)
        {
            cup.SetActive(false);
        }
        for (int i = 0; i < currentCups; i++)
        {
            cupList[i].SetActive(true);
        }
    }
}
