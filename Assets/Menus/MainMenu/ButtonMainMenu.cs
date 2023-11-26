using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    public void ButtonPlay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("GameScene");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}
