using UnityEngine;

[CreateAssetMenu(menuName = "Modifications/Recipe")]
public class RecipeModificationSO : ModificationSO
{
    public RecipeSO recipe;
    [Range(0f, 0.3f)] public float customerModifier;
    public override void Apply()
    {
        OrderManager.Instance.AddRecipe(recipe);
        GameManager.Instance.customerModifier += customerModifier;
    }
}
