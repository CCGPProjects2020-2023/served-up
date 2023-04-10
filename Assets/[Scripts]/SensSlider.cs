using UnityEngine;
using UnityEngine.UI;

public class SensSlider : MonoBehaviour
{
    private Slider sensSlider;

    private void Awake()
    {
        sensSlider = GetComponentInChildren<Slider>();
    }
    public void OnSliderValueChanged()
    {
        PlayerPrefs.SetInt("Sensitivity", (int)sensSlider.value);
        PlayerPrefs.Save();
        PlayerLook.UpdateSensitivity();
    }

    private void OnEnable()
    {
        sensSlider.value = PlayerPrefs.GetInt("Sensitivity", 20);
    }
}
