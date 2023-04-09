using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
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
    public int startingCustomers;
    public int currentCustomers;
    public float customerModifier;
    public int decreaseCustomerMod;
    public int increaseCustomerMod;
    public int startingDay;
    public int currentDay;

    [Header("Timers")]
    public float thinkingTime = 2.5f;
    public float serviceTime = 150;
    public float deliveryTime = 90;
    public float eatingTime = 3;

    private void Start()
    {
        startingDay = 1;
        currentDay = startingDay;
        customerModifier = 1;
    }

    public void StartGame()
    {
        FindObjectOfType<PlayerController>(true).gameObject.SetActive(true);
        currentCustomers = CalculateCustomerAmount();
        Events.onDayStarted.Invoke();
    }

    public void OnDayCompleted()
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.dayCompletedSound, transform.position);
        //choose modification - then start next day
        ModificationManager.Instance.GenerateModificationOptions();
    }

    public void EndDay()
    {
        //do end of day stuff -- fade out or whatever
        StartNewDay();
    }

    public void StartNewDay()
    {
        currentDay++;
        currentCustomers = CalculateCustomerAmount();
        Events.onDayStarted.Invoke();
    }

    private int CalculateCustomerAmount()
    {
        customerModifier = (float)(Mathf.Pow(0.99f, decreaseCustomerMod) + 0.99 - Mathf.Pow(0.99f, increaseCustomerMod));
        float baseMultiplier = 1f;
        float courseAmount = 1.25f;
        float dayModifier = currentDay switch
        {
            1 => 1.25f,
            2 => 1.5f,
            3 => 1.75f,
            _ => 1 + (0.2f * (currentDay - 3)),
        };
        //(Day Length) / 25 * (Customer Modifier Rate) * (Day Modifier) * (player Modifier) / (Course Amount)			

        return Mathf.RoundToInt(CalculateDayLength() / 25 * customerModifier * dayModifier * baseMultiplier / courseAmount);

        //return Mathf.RoundToInt(customerModifier * (0.0291f * Mathf.Pow(currentDay, 2) + 0.6917f * currentDay + 2.4615f));
    }

    public float CalculateDayLength()
    {
        return 100 + 25 * Mathf.Floor((currentDay - 1) / 3f);
    }
    private void OnEnable()
    {
        Events.onDayCompleted.AddListener(OnDayCompleted);
    }
    private void OnDisable()
    {
        Events.onDayCompleted.RemoveListener(OnDayCompleted);
    }
}
