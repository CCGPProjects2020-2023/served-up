using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject recipeMenu;
    public GameObject crosshair;
    public GameObject gameOverMenu;


    public void TogglePause()
    {
        if (GameManager.Instance.isPaused)
        {
            recipeMenu.SetActive(false);
            settingsMenu.SetActive(false);
            pauseMenu.SetActive(false);
            crosshair.SetActive(true);
            GameManager.Instance.ResumeGame();
        }
        else
        {
            pauseMenu.SetActive(true);
            crosshair.SetActive(false);
            GameManager.Instance.PauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        crosshair.SetActive(false);
        GameManager.Instance.PauseGame();
    }
}
