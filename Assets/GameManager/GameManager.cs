using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;
    private int _boundOffset = 7;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private bool _isFinish = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _minBound = Camera.main.ViewportToWorldPoint(Vector2.zero);
        _maxBound = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    private void Update()
    {
        if (PlayerStats.Instance.Life <= 0 && !_isFinish)
        {
            _isFinish = true;
            PausesGame();
            GameOver();
        }
    }

    public bool CheckInScreen(Vector2 position)
    {
        if (position.x > _maxBound.x + _boundOffset)
        {
            return true;
        }
        else if (position.x < _minBound.x - _boundOffset)
        {
            return true;
        }
        else if (position.y > _maxBound.y + _boundOffset)
        {
            return true;
        }
        else if (position.y < _minBound.y - _boundOffset)
        {
            return true;
        }
        else
            return false;
    }

    public void PausesGame()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !Cursor.visible;
    }

    private void GameOver()
    {
        gameOverMenu.SetActive(true);
        PlayerStats.Instance.StopAttack();
    }

    public Vector2 MinBound { get { return _minBound; } }
    public Vector2 MaxBound { get { return _maxBound; } }
    public int BoundOffset { get { return _boundOffset; } }
}
