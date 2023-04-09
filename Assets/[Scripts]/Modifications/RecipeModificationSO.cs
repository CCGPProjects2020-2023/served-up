using UnityEngine;

[CreateAssetMenu(menuName = "Modifications/Recipe")]
public class RecipeModificationSO : ModificationSO
{
    public RecipeListSO recipe;
    [Tooltip("Amount to decrease total customer amount when recipe is added - 1 = 1% decrease")]
    [Range(0, 30)] public int decreaseAmount;
    public override void Apply()
    {
        OrderManager.Instance.AddRecipe(recipe);
        GameManager.Instance.decreaseCustomerMod += decreaseAmount;
    }
}
