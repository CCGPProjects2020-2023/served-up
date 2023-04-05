using System.Collections.Generic;
using UnityEngine;

public class ModificationManager : MonoBehaviour
{
    public static ModificationManager Instance { get; private set; }
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

        availableModifications = new List<ModificationSO>();
        availableModifications.AddRange(allModifications);
        options = new List<GameObject>();
    }

    public List<GameObject> boardPositions;
    public GameObject chosenModification;
    public List<ModificationSO> allModifications;
    public List<ModificationSO> availableModifications;
    public GameObject modificationPrefab;
    public List<ModificationSO> enabledModifications;
    public GameObject button;

    private List<GameObject> options;

    public void GenerateModificationOptions()
    {
        button.gameObject.SetActive(true);
        foreach (GameObject pos in boardPositions)
        {
            if (availableModifications.Count > 0)
            {
                Placeable placeable = pos.GetComponent<Placeable>();
                GameObject modObj = Instantiate(modificationPrefab, placeable.itemPos.transform);
                options.Add(modObj);
                placeable.item = modObj;
                ModificationSOHolder holder = modObj.GetComponent<ModificationSOHolder>();
                holder.modificationSO = RandomModifier();
            }
        }
    }

    private void Start()
    {

    }

    public void LockInModifications()
    {
        Placeable placeable = chosenModification.GetComponent<Placeable>();
        if (placeable.item == null)
        {
            return;
        }

        ModificationSO chosenMod = placeable.item.GetComponent<ModificationSOHolder>().modificationSO;


        foreach (GameObject obj in options)
        {
            if (chosenMod != obj)
            {
                availableModifications.Add(obj.GetComponent<ModificationSOHolder>().modificationSO);
            }
        }

        chosenMod.Apply();
        enabledModifications.Add(chosenMod);
        ResetBoard();
        button.gameObject.SetActive(false);
        GameManager.Instance.EndDay();
    }

    public void ResetBoard()
    {
        foreach (GameObject obj in options)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in boardPositions)
        {
            obj.GetComponent<Placeable>().item = null;
        }
        chosenModification.GetComponent<Placeable>().item = null;
    }

    public ModificationSO RandomModifier()
    {
        // gets random modification from list of available 
        ModificationSO mod = availableModifications[Random.Range(0, availableModifications.Count)];
        availableModifications.Remove(mod); // removes chosen modification so it can't show up again
        return mod;
    }

}
