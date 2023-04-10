using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public List<ItemSO> orderList;

    public List<ItemSO> ingredientList;

    public List<RecipeListSO> recipeList;

    public List<ItemSO> icedDrinks;

    public ItemSO iceBucket;
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
        ItemSO order = orderList[Random.Range(0, orderList.Count)];

        return order;
    }
    public void AddRecipe(RecipeListSO recipe)
    {
        recipeList.Add(recipe);
        UpdateOrderList();
    }

    public void AddIngredient(ItemSO item)
    {
        ingredientList.Add(item);
        UpdateOrderList();
    }

    public void UpdateOrderList()
    {
        orderList.Clear();
        foreach (RecipeListSO recipe in recipeList)
        {
            orderList.Add(recipe.baseRecipe.output);
            foreach (RecipeSO variation in recipe.variations)
            {
                if (icedDrinks.Contains(variation.output) && !ingredientList.Contains(iceBucket))
                {
                    Debug.Log(variation.output.name);
                }
                else if (ingredientList.Contains(variation.input1) || ingredientList.Contains(variation.input2))
                    orderList.Add(variation.output);
            }
        }
    }
}
