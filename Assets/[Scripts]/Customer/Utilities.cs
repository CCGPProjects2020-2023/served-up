using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static float GetExpDistValue(float lambda)
    {
        return -Mathf.Log(1 - Random.value) / lambda;
    }
}
