using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: Header("Order Complete SFX")]
    [field: SerializeField] public EventReference orderCompleteSound { get; private set; }

    [field: Header("Footstep SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Level Music")]
    [field: SerializeField] public EventReference levelMusic { get; private set; }



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
