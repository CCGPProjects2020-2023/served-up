using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModificationSO : ScriptableObject
{
    public Sprite icon;
    public string modificationName;
    public string description;

    public abstract void Apply();
}
