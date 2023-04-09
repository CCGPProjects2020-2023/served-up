using UnityEngine;

[CreateAssetMenu(menuName = "Modifications/Customer Amount")]
public class CustomerAmountModificationSO : ModificationSO
{
    [Range(0, 30)] public int increaseAmount;
    public override void Apply()
    {
        GameManager.Instance.increaseCustomerMod += increaseAmount;
    }
}
