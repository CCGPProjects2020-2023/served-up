using System.Collections;
using System.Collections.Generic;
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
    public int startingDay;
    public int currentDay;


    private void Start()
    {
        startingCustomers = 4;
        currentCustomers = startingCustomers;
        startingDay = 1;
        currentDay = startingDay;
        Events.onDayStarted.Invoke();
    }

    public void OnDayCompleted()
    {
        //choose modification - then start next day
        StartNewDay();
    }

    public void StartNewDay()
    {
        currentDay++;
        Events.onDayStarted.Invoke();
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
