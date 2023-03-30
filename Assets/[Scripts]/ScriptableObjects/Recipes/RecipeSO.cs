using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeScriptableObject")]
public class RecipeSO : ScriptableObject
{
    public ItemSO input1;
    public ItemSO input2;
    public ItemSO output;
}
