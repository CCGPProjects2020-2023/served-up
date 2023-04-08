using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuCamera;
    [SerializeField] private PlayableDirector timeline;
    public void PlayGame()
    {
        StartCoroutine(StartTimeline());
        mainMenu.SetActive(false);
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
}
