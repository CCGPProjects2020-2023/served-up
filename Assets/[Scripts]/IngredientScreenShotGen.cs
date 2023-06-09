#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine;


public class IngredientScreenShotGen : MonoBehaviour
{
    public GameObject[] objsToScreenshot;
    GameObject currentObj;
    private void Start()
    {
        // for (int i = 0; i < ingredientsToScreenShot.Length; i++)
        // {
        //     CreateMyAsset(ingredientsToScreenShot[i].gameObject.name);   
        // }
        StartCoroutine(GenerateScreenShots());
        //ApplySprites();
    }
    public IEnumerator GenerateScreenShots()
    {
        yield return new WaitForSeconds(1f);
        foreach (GameObject item in objsToScreenshot)
        {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            currentObj = item;
            ScreenshotHandler.TakeScreenshot_Static(512, 512);
            EditorUtility.SetDirty(currentObj);

            yield return new WaitForSeconds(.5f);
            item.gameObject.SetActive(false);

        }
    }
    /* public void ApplySprites()
     {
         foreach (Ingredient item in ingredients)
         {
             item.ingredientIcon =  (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Ingredients/Icons/"+item.name, typeof(Sprite));
             EditorUtility.SetDirty(item);
         }
         AssetDatabase.SaveAssets();
     }*/
    /* public static void CreateMyAsset(string i)
     {
         Ingredient asset = ScriptableObject.CreateInstance<Ingredient>();

         AssetDatabase.CreateAsset(asset,"Assets/Art/Ingredients/"+i.Replace(" ", "")+".asset");
         AssetDatabase.SaveAssets();

         EditorUtility.FocusProjectWindow();

         Selection.activeObject = asset;
     }*/

    public string GetFileName()
    {
        return "Assets/Icons/" + currentObj.name;
    }
}

#endif