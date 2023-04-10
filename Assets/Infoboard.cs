using TMPro;
using UnityEngine;

public class Infoboard : MonoBehaviour
{
    public GameObject enabledModPrefab;

    public GameObject modificationCanvas;

    public TextMeshProUGUI dayText, customerText;

    private void Start()
    {
        foreach (ModificationSO modificationSO in ModificationManager.Instance.enabledModifications)
        {
            GameObject modObj = Instantiate(enabledModPrefab, modificationCanvas.transform);

            ModificationSOHolder holder = modObj.GetComponentInChildren<ModificationSOHolder>();

            holder.modificationSO = modificationSO;
        }
    }
    private void OnModificationAdded(ModificationSO obj)
    {
        GameObject modObj = Instantiate(enabledModPrefab, modificationCanvas.transform);
        ModificationSOHolder holder = modObj.GetComponentInChildren<ModificationSOHolder>();
        holder.modificationSO = obj;
    }
    private void OnDayStarted()
    {
        dayText.text = GameManager.Instance.currentDay.ToString();
        customerText.text = GameManager.Instance.currentCustomers.ToString();
    }
    private void OnEnable()
    {
        Events.onModificationAdded.AddListener(OnModificationAdded);
        Events.onDayStarted.AddListener(OnDayStarted);
    }

    private void OnDisable()
    {
        Events.onModificationAdded.RemoveListener(OnModificationAdded);
        Events.onDayStarted.RemoveListener(OnDayStarted);
    }
}
