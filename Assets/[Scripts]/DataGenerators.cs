using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataGenerators
{
    public static float GetExpDistValue(float lambda)
    {
        return -Mathf.Log(1 - Random.value) / lambda;
    }
}
