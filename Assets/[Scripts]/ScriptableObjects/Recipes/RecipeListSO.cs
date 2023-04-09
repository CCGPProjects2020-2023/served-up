using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeListScriptableObject")]
public class RecipeListSO : ScriptableObject
{
    public RecipeSO baseRecipe;
    public List<RecipeSO> variations;
}
