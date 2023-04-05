using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    private PlayableDirector timeline;
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
        timeline.played += Director_Played;
        //timeline.stopped += Director_Stopped;

    }

    private void Director_Played(PlayableDirector obj)
    {
        menuPanel.SetActive(false);
    }
    public void StartTimeline()
    {
        timeline.Play();
    }
}
