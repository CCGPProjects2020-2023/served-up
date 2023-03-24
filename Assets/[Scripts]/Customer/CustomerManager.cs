using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public static int NrSamples = 500;
    public float Lambda = 12f; // number of arrivals per hour on average
    public float Mu = 20f; // number of customers serviced per hour on average
    public List<Customer> customers = new();
    public List<CustomerMovement> customersScene;
    public List<GameObject> tablesList;
    public GameObject customerPrefab;

    public float totalTimeElasped;
    public float timeSinceArrival;
    public float timeTillNextArrival;
    public float timeTillServiced;
    public float timeService;
    public float timeScale; // to change time scale set this
    public int currentCustomer;
    public int customersServiced;
    public bool isBeingServiced;

    // Start is called before the first frame update
    void Start()
    {
        GenerateCustomers();
        tablesList = GameObject.FindGameObjectWithTag("TableList").GetComponent<TablesList>().tables;
        isBeingServiced = false;
        customersServiced = 0;
        timeService = 0;
        currentCustomer = 0;
        totalTimeElasped = 0;
        timeSinceArrival = 0;
        Time.timeScale = timeScale;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CustomerArrival();
        timeSinceArrival += Time.fixedDeltaTime;
    }

    private void CustomerArrival()
    {
        // gets the interarrival time of the currentcustomer checks if it is
        // <= the time since last arrival (this value gets reset to 0 after a customer arrives)
        if (customers[currentCustomer].GetInterarrivalTime() <= timeSinceArrival / 60)
        {
            //see below
            CreateCustomer();
            //increments the current customer
            currentCustomer++;
            //resets the time to 0
            timeSinceArrival = 0;
        }
    }

    private void CreateCustomer()
    {
        List<Table> tables = new List<Table>();
        // gets the Waypoint script from the list of waypointsList (basically is just converting the gameobject list(waypointsList) to a list of the script(Waypoint) on the gameobjects)
        // this allows us to check the isEmpty property on each waypoint
        foreach (GameObject waypoint in tablesList)
            tables.Add(waypoint.GetComponent<Table>());

        // loops through all waypoints
        for (int i = 0; i < tables.Count; i++)
        {
            // finds first empty waypoint starting at the first waypoint(ATM)
            if (tables[i].isEmpty)
            {
                // checks if the waypoint is the ATM
                if (i == 0)
                {
                    // customer is spawned at ATM a.k.a. the queue is empty
                    // resets the timeService to properly track how long they need to be serviced
                    timeService = 0;
                    // used to activate an if statement condition for servicing (will be explained in service section)
                    isBeingServiced = true;
                }
                //instantiates a new customer at the position of waypoints[i] and stores it in a temp variable
                GameObject customer = Instantiate(customerPrefab, tables[i].transform.position, tables[i].transform.rotation);

                // sets the waypoint index on the customer's movement script, used to keep track of which waypoint to go to next in the movement script
                customer.GetComponent<CustomerMovement>().currentTableIndex = i;
                // adds the new customer to a list used to keep track of all customers who are currently in queue or being serviced
                customersScene.Add(customer.GetComponent<CustomerMovement>());
                tables[i].isEmpty = false; // updates the waypoint to be not empty
                break; // breaks from for loop once an empty waypoint is found
            }
        }
    }

    void GenerateCustomers()
    {
        for (int i = 0; i < NrSamples; i++)
        {
            float arrivalTimeInMin = Utilities.GetExpDistValue(Lambda) * 60f; // 1 / 5 * 60 = 12 (5 is the mean inter-arrival time) 
            //float serviceTimeInMin = Utilities.GetExpDistValue(Mu) * 60f; // 1/ 3 * 60 = 20 (3 is the mean service time)
                                                                          // adds the values to a list of customers - customers have two properties arrivalTime and serviceTime
            customers.Add(new Customer(arrivalTimeInMin));
            Debug.Log("arrivalTimeInMin: " + arrivalTimeInMin);
            
        }
    }
}

