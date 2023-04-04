using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModificationUI : MonoBehaviour
{
    private ModificationSO modificationSO;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI positivesText;
    public TextMeshProUGUI negativesText;
    public Image icon;

    private void Start()
    {
        modificationSO = GetComponentInParent<ModificationSO>();
        SetupUI();
    }

    private void SetupUI()
    {
        titleText.text = modificationSO.title;
        descriptionText.text = modificationSO.description;
        icon.sprite = modificationSO.icon;

        positivesText.text = modificationSO.positives[0];
        modificationSO.positives.RemoveAt(0);
        foreach (string str in modificationSO.positives)
        {
            positivesText.text += "\n" + str;
        }

        negativesText.text = modificationSO.negatives[0];
        modificationSO.negatives.RemoveAt(0);
        foreach (string str in modificationSO.negatives)
        {
            negativesText.text += "\n" + str;
        }
    }
}
