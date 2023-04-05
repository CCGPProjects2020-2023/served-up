using System.Collections.Generic;
using UnityEngine;

public abstract class ModificationSO : ScriptableObject
{
    public Sprite icon;
    public string title;
    public string description;
    public List<string> positives;
    public List<string> negatives;

    public abstract void Apply();
}
