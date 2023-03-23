using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectScriptableObject : ScriptableObject
{
    // read-only data 
    public Transform prefab; 

    public string objectName;
}
