using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonGameMenu : MonoBehaviour
{
    public void ButtonPauseResume()
    {
        GameManager.Instance.PausesGame();
    }

    public void ButtonRetry()
    {
        GameManager.Instance.PausesGame();
        SceneManager.LoadScene("GameScene");
    }

    public void ButtonExit()
    {
        GameManager.Instance.PausesGame();
        SceneManager.LoadScene("MainMenu");
    }
}
