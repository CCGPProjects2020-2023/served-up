using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemSO : ScriptableObject
{
    public string name;
    public Sprite icon;
    public GameObject prefab;
    public bool isDestructable;
}
