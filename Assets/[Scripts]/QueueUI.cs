using UnityEngine;
using UnityEngine.UI;

public class QueueUI : MonoBehaviour
{
    private CustomerManager customerManager;
    [SerializeField] private Image barImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;

    public float fillAmount;
    // Start is called before the first frame update
    void Start()
    {
        customerManager = GetComponentInParent<CustomerManager>();
        fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (customerManager.AmountOfCustomersInQueue() > 0)
        {
            ShowBar();
            fillAmount = customerManager.currentQueueTimer / customerManager.maxWaitTime;
            barImage.fillAmount = fillAmount;
        }
        else
        {
            HideBar();
            fillAmount = 1f;
        }
    }

    private void HideBar()
    {
        barImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
        iconImage.gameObject.SetActive(false);
    }

    private void ShowBar()
    {
        barImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
        iconImage.gameObject.SetActive(true);
    }
}
