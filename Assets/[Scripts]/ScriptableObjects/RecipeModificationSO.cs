using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifications/Recipe")]
public class RecipeModificationSO : ModificationSO
{
    public RecipeSO recipe;
    public override void Apply()
    {
        RecipeSystem.Instance.AddRecipe(recipe);
    }
}
