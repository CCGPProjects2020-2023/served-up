using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    private Table table;
    [SerializeField] private Image barImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;

    [SerializeField] private Sprite thinkingImage;
    [SerializeField] private Sprite serviceImage;
    [SerializeField] private Sprite deliveryImage;
    [SerializeField] private Sprite eatingImage;
    public float fillAmount;
    // Start is called before the first frame update
    void Start()
    {
        table = GetComponentInParent<Table>();
        OnTableStateChanged(table, table.currentState);
        fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (table.currentState == TableState.Service || table.currentState == TableState.Delivery)
        {
            fillAmount = table.timer / table.currentTimer;
            barImage.fillAmount = fillAmount;
        }
    }
    private void OnTableStateChanged(Table table, TableState state)
    {
        if (this.table != table)
            return;
        switch (state)
        {
            case TableState.None:
                HideBar();
                SetIconImage(null);
                break;
            case TableState.Thinking:
                HideBar();
                SetIconImage(thinkingImage);
                break;
            case TableState.Service:
                ShowBar();
                SetIconImage(serviceImage);
                break;
            case TableState.Delivery:
                ShowBar();
                SetIconImage(deliveryImage);
                break;
            case TableState.Eating:
                HideBar();
                SetIconImage(eatingImage);
                break;
        }
    }
    private void SetIconImage(Sprite sprite)
    {
        if (sprite == null)
        {
            iconImage.gameObject.SetActive(false);
            return;
        }
        iconImage.gameObject.SetActive(true);
        iconImage.sprite = sprite;
    }
    private void HideBar()
    {
        barImage.gameObject.SetActive(false);
        backgroundImage.gameObject.SetActive(false);
    }

    private void ShowBar()
    {
        barImage.gameObject.SetActive(true);
        backgroundImage.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Events.onTableStateChanged.AddListener(OnTableStateChanged);
    }

    private void OnDisable()
    {
        Events.onTableStateChanged.RemoveListener(OnTableStateChanged);
    }
}
