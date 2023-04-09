using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameOver : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    public void OnGameOver()
    {
        timeline.Play();
    }

    private void OnEnable()
    {
        Events.onGameOver.AddListener(OnGameOver);
    }
    private void OnDisable()
    {
        Events.onGameOver.AddListener(OnGameOver);
    }
}
