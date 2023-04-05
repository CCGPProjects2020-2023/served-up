using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<ItemSO> itemSOList;
    public static OrderManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public ItemSO GenerateOrder()
    {
        ItemSO order = itemSOList[Random.Range(0, itemSOList.Count)];

        return order;
    }
    public void AddRecipe(RecipeSO recipe)
    {
        itemSOList.Add(recipe.output);
    }
}
