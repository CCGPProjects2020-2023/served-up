using System.Collections.Generic;
using UnityEngine;

public class RecipeSystem : MonoBehaviour
{
    public static RecipeSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    public List<RecipeSO> recipes;
    public ItemSO GetRecipeOutput(ItemSO item1, ItemSO item2)
    {
        foreach (RecipeSO recipe in recipes)
        {
            if (recipe.input1 == item1 || recipe.input1 == item2)
            {
                if (recipe.input2 == item1 || recipe.input2 == item2)
                {
                    return recipe.output;
                }
            }
        }
        return null;
    }
}
