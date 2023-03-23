using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptableObject kitchenScriptableObject;

    public KitchenObjectScriptableObject GetKitchenScriptableObject()
    {
        return kitchenScriptableObject;
    }

}
