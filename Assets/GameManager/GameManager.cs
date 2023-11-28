using System;
using System.Collections;
using System.Linq;
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
    private bool _isPaused = false;
    
    private float _startTime = 0;

    [SerializeField] private WaveManager.Phase _phase = WaveManager.Phase.low;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _minBound = Camera.main.ViewportToWorldPoint(Vector2.zero);
        _maxBound = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (PlayerStats.Instance.Life <= 0 && !_isFinish)
        {
            _isFinish = true;
            SwitchTime();
            SwitchCursor();
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

    public void UpdatePhase()
    {
        if (PlayerStats.Instance.Score >= ((float)_phase))
        {
            _phase = Enum.GetValues(typeof(WaveManager.Phase)).Cast<WaveManager.Phase>().SkipWhile(e => e != _phase).Skip(1).First();

            if (_phase == WaveManager.Phase.mid)
            {
                StartCoroutine(LaunchBonusSpawner());
            }
        }
    }

    private IEnumerator LaunchBonusSpawner()
    {
        yield return new WaitUntil(() => !DemonSpawner.Instance.IsSpawning);
        BonusSpawner.Instance.CanSpawn = true;
    }

    public void PausesGame()
    {
        _isPaused = !_isPaused;
        pauseMenu.SetActive(_isPaused);
        SwitchTime();
        SwitchCursor();

        if (_isPaused) 
            PlayerStats.Instance.StopAttack();
        else 
            PlayerStats.Instance.StartAttack();
    }

    public void SwitchTime()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    public void SwitchCursor()
    {
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
    public float StartTime { get { return _startTime; } }
    public WaveManager.Phase Phase { get { return _phase; } }
}
