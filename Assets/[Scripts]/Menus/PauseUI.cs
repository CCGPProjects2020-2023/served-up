using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject crosshair;
    public void TogglePause()
    {
        if (GameManager.Instance.isPaused)
        {
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
