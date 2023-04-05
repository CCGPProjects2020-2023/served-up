using System.Collections.Generic;
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
        modificationSO = GetComponentInParent<ModificationSOHolder>().modificationSO;
        SetupUI();
    }

    private void SetupUI()
    {
        titleText.text = modificationSO.title;
        descriptionText.text = modificationSO.description;
        icon.sprite = modificationSO.icon;

        List<string> positives = new List<string>();
        positives.AddRange(modificationSO.positives);
        positivesText.text = positives[0];
        positives.RemoveAt(0);
        foreach (string str in positives)
        {
            positivesText.text += "\n" + str;
        }

        List<string> negatives = new List<string>();
        negatives.AddRange(modificationSO.negatives);
        negativesText.text = negatives[0];
        negatives.RemoveAt(0);
        foreach (string str in negatives)
        {
            negativesText.text += "\n" + str;
        }
    }
}
