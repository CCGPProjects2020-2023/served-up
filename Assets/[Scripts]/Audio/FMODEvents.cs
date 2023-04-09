using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents Instance { get; private set; }

    [field: Header("Order Complete SFX")]
    [field: SerializeField] public EventReference orderCompleteSound { get; private set; }

    [field: Header("Footstep SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Pickup SFX")]
    [field: SerializeField] public EventReference pickupSound { get; private set; }

    [field: Header("Fill Cup SFX")]
    [field: SerializeField] public EventReference fillCupSound { get; private set; }

    [field: Header("Ice SFX")]
    [field: SerializeField] public EventReference iceSound { get; private set; }

    [field: Header("Clean Cup SFX")]
    [field: SerializeField] public EventReference cleanCupSound { get; private set; }

    [field: Header("Drinking SFX")]
    [field: SerializeField] public EventReference drinkingSound { get; private set; }

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
