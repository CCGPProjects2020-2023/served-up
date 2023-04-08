using UnityEngine;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject menuCamera;
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        menuCamera.SetActive(false);
        GameManager.Instance.StartGame();
    }
}
