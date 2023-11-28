using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    public void ButtonPlay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;

        SceneManager.LoadScene("GameScene");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
