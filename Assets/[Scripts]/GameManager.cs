using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winText;
    private void OnEnable()
    {
        Events.onGameWon.AddListener(GameWon);
    }
    private void OnDisable()
    {
        Events.onGameWon.RemoveListener(GameWon);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWon()
    {
        winText.gameObject.SetActive(true);
    }
}
