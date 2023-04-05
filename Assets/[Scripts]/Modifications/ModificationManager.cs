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
    }

    public List<GameObject> boardPositions;
    public GameObject chosenModification;
    public List<ModificationSO> allModifications;
    public List<ModificationSO> availableModifications;
    public GameObject modificationPrefab;
    public List<ModificationSO> enabledModifications;

    public void GenerateModificationOptions()
    {
        foreach (GameObject pos in boardPositions)
        {
            if (availableModifications.Count > 0)
            {
                Placeable placeable = pos.GetComponent<Placeable>();
                GameObject modObj = Instantiate(modificationPrefab, placeable.itemPos.transform);
                placeable.item = modObj;
                ModificationSOHolder holder = modObj.GetComponent<ModificationSOHolder>();
                holder.modificationSO = RandomModifier();
            }
        }
    }

    private void Start()
    {
        GenerateModificationOptions();
    }

    public void LockInModifications()
    {
        Placeable placeable = chosenModification.GetComponent<Placeable>();
        ModificationSO chosenMod = placeable.item.GetComponent<ModificationSOHolder>().modificationSO;

        ModificationSO option1 = boardPositions[0].GetComponent<Placeable>().item.GetComponent<ModificationSOHolder>().modificationSO;
        ModificationSO option2 = boardPositions[1].GetComponent<Placeable>().item.GetComponent<ModificationSOHolder>().modificationSO;

        if (chosenMod != option1)
        {
            availableModifications.Add(option1);
        }
        else if (chosenMod != option2)
        {
            availableModifications.Add(option2);
        }

        chosenMod.Apply();
        enabledModifications.Add(chosenMod);

        ClearOptions();
    }

    public void ClearOptions()
    {
        foreach (GameObject pos in boardPositions)
        {
            Placeable placeable = pos.GetComponent<Placeable>();

            Destroy(placeable.item);
            placeable.item = null;
        }
    }

    public ModificationSO RandomModifier()
    {
        // gets random modification from list of available 
        ModificationSO mod = availableModifications[Random.Range(0, availableModifications.Count)];
        availableModifications.Remove(mod); // removes chosen modification so it can't show up again
        return mod;
    }

}
