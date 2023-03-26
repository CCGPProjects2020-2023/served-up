using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeModel : MonoBehaviour
{
    public List<GameObject> modelsList;

    private void Start()
    {
        modelsList[Random.Range(0, modelsList.Count)].SetActive(true);
    }
}
