using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private PlayableDirector timeline;
    public void PlayGame()
    {
        StartCoroutine(StartTimeline());
        menuCanvas.SetActive(false);
    }

    IEnumerator StartTimeline()
    {
        timeline.Play();
        while (true)
        {
            if (timeline.state != PlayState.Playing)
            {
                menuCamera.SetActive(false);
                GameManager.Instance.StartGame();
                break;
            }
            yield return null;
        }
    }

    public void ToggleSettingsMenu()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
}
