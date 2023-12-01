using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] ScreenBound = new GameObject[2];
    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;
    private int _boundOffset = 7;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    private bool _isFinish = false;
    private bool _isPaused = false;
    
    private float _startTime = 0;

    private bool _inGame = true;
    private float _gameTime = 0;
    private bool _inBoss = false;
    private bool _isBossFightStarted = false;
    private float _bossTime = 0;

    [SerializeField] private WaveManager.Phase _phase = WaveManager.Phase.low;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _maxBound = ScreenBound[0].transform.position;
        _minBound = ScreenBound[1].transform.position;
    }

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if ((PlayerStats.Instance.Life <= 0 || ParadiseGateStats.Instance.Life <= 0) && !_isFinish)
        {
            _isFinish = true;
            SwitchTime();
            SwitchCursor();
            GameOver();
        }
        UpdatePhase();

        _gameTime += _inGame ? Time.deltaTime : 0;
        _bossTime += _inBoss ? Time.deltaTime : 0;
    }

    public bool CheckInScreen(Vector2 position)
    {
        if (position.x > _maxBound.x)
        {
            return false;
        }
        else if (position.x < _minBound.x)
        {
            return false;
        }
        else if (position.y > _maxBound.y)
        {
            return false;
        }
        else if (position.y < _minBound.y)
        {
            return false;
        }
        else
            return true;
    }

    public bool CheckInScreenWithOffset(Vector2 position)
    {
        if (position.x > _maxBound.x + _boundOffset)
        {
            return false;
        }
        else if (position.x < _minBound.x - _boundOffset)
        {
            return false;
        }
        else if (position.y > _maxBound.y + _boundOffset)
        {
            return false;
        }
        else if (position.y < _minBound.y - _boundOffset)
        {
            return false;
        }
        else
            return true;
    }

    public void UpdatePhase()
    {
        if (_gameTime >= (float)_phase)
        {
            if (_phase < WaveManager.Phase.ultimate1 && !_isBossFightStarted)
            {
                _isBossFightStarted = true;
                StartCoroutine(BossFight());
            }
            else if (_phase >= WaveManager.Phase.ultimate1)
            {
                NextPhase();
            }
        }
    }

    private void NextPhase()
    {
        _phase = Enum.GetValues(typeof(WaveManager.Phase)).Cast<WaveManager.Phase>().SkipWhile(e => e != _phase).Skip(1).First();
        BonusSpawner.Instance.UpdateBonusCanBeSpawn();
    }

    private IEnumerator BossFight()
    {
        _inGame = false;
        _inBoss = true;
        DemonSpawner.Instance.DoSpawnBoss();
        BonusSpawner.Instance.CanSpawn = false;

        yield return new WaitWhile(() => DemonSpawner.Instance.DemonInScreen);
        yield return new WaitWhile(() => DemonSpawner.Instance.BossIsAlive());
        
        _inGame = true;
        _inBoss = false;
        _isBossFightStarted = false;
        NextPhase();
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

    #region Get
    public Vector2 MinBound { get { return _minBound; } }
    public Vector2 MaxBound { get { return _maxBound; } }
    public int BoundOffset { get { return _boundOffset; } }
    public float StartTime { get { return _startTime; } }
    public float AllTime { get { return _gameTime + _bossTime; } }
    public bool InGame { get { return _inGame; } }
    public WaveManager.Phase Phase { get { return _phase; } }
    #endregion

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PausesGame();
        }
    }
}
