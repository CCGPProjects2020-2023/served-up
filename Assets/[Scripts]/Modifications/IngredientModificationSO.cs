using UnityEngine;

[CreateAssetMenu(menuName = "Modifications/Ingredient")]
public class IngredientModificationSO : ModificationSO
{
    public ItemSO ingredient;
    [Tooltip("Amount to decrease total customer amount when ingredient is added - 1 = 1% decrease")]
    [Range(0, 30)] public int decreaseAmount;
    public override void Apply()
    {
        OrderManager.Instance.AddIngredient(ingredient);
        GameManager.Instance.decreaseCustomerMod += decreaseAmount;
    }
}
