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
    }

    public List<GameObject> boardPositions;
    public GameObject chosenModification;
    public List<ModificationSO> modifications;
    public GameObject modificationPrefab;

    public void GenerateModificationOptions()
    {
        foreach (GameObject pos in boardPositions)
        {
            Placeable placeable = pos.GetComponent<Placeable>();
            GameObject modObj = Instantiate(modificationPrefab, placeable.transform);
            ModificationSOHolder holder = modObj.GetComponent<ModificationSOHolder>();
            holder.modificationSO = RandomModifier();
        }
    }

    public void LockInModifications()
    {
        Placeable placeable = chosenModification.GetComponent<Placeable>();
        ModificationSO chosenMod = placeable.item.GetComponent<ModificationSOHolder>().modificationSO;
    }

    public ModificationSO RandomModifier()
    {
        // gets random modification from list of available 
        ModificationSO mod = modifications[Random.Range(0, modifications.Count)];
        modifications.Remove(mod); // removes chosen modification so it can't show up again
        return mod;
    }

}
