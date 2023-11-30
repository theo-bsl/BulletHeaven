using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    public static DemonSpawner Instance;

    private Vector2 spawnPoint = Vector2.zero;
    private Vector2 bossSpawnPoint = Vector2.zero;
    private Vector2 spawnLimit = Vector2.zero;
    private float _spawnTime = 0;
    private int _waitSpawn = 0;
    private bool _isSpawning = false;
    private bool _demonInScreen = false;
    private bool _haveToSpawnBoss = false;

    public GameObject[] DemonsPrefab = new GameObject[4];
    private List<GameObject> _demonsSpawn;

    public GameObject[] BossPrefab = new GameObject[4];
    private int _indexBossToSpawn = 0;
    private GameObject _boss = null;

    private float[] timeWaves = null;
    private int[] minMaxNbDemon = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        spawnLimit.x = GameManager.Instance.MinBound.x + 3;
        spawnLimit.y = GameManager.Instance.MaxBound.x - 3;
        spawnPoint.y = GameManager.Instance.MaxBound.y + 6.5f;

        //bossSpawnPoint.y = spawnPoint.y;

        _waitSpawn = WaveManager.WaitSpawn;

        timeWaves = WaveManager.TimeWaves;
        minMaxNbDemon = WaveManager.MinMaxNbDemon;

        _demonsSpawn = new List<GameObject>();
    }

    private void Update()
    {
        _demonInScreen = CheckDemonSpawnInScreen();
        if (!_demonInScreen && !_isSpawning)
        {
            if (_haveToSpawnBoss)
            {
                SpawnBoss();
            }
            else if (Time.time > _spawnTime && GameManager.Instance.InGame)
            {
                _isSpawning = true;
                _demonsSpawn.Clear();
                StartCoroutine(SpawnWave());
            }
        }
    }

    private IEnumerator SpawnWave()
    {
        float playerScore = PlayerStats.Instance.Score;
        int[] nbDemonsToSpawn = SetNbDemonsToSpawn();
        int nbAllDemons = GetNbAllDemonsToSpawn(nbDemonsToSpawn);
        float delayBetweenSpawn = GetDelayBetweenSpawn(nbAllDemons);
        int[] nbDemonSpawn = SetArrayNbDemonSpawn();
        List<int> indexDemonCanBeSpawn = GetDemonsCanBeSpawn();
        int indexDemon = 0;

        for (int i = 0; i < nbAllDemons; i++)
        {
            indexDemon = GetIndexDemonToSpawn();
            _demonsSpawn.Add(SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonMid));
            yield return new WaitForSeconds(delayBetweenSpawn);
        }
        _spawnTime = Time.time + _waitSpawn;

        _isSpawning = false;
    }

    private int[] SetNbDemonsToSpawn()
    {
        switch (GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: 
                return new int[1] { Random.Range(minMaxNbDemon[0], minMaxNbDemon[1]) };

            case WaveManager.Phase.mid: 
                return new int[2] { Random.Range(minMaxNbDemon[2], minMaxNbDemon[3]), Random.Range(minMaxNbDemon[4], minMaxNbDemon[5]) };
            
            case WaveManager.Phase.high: 
                return new int[3]{Random.Range(minMaxNbDemon[6], minMaxNbDemon[7]),Random.Range(minMaxNbDemon[8], minMaxNbDemon[9]),Random.Range(minMaxNbDemon[10], minMaxNbDemon[11])};

            case WaveManager.Phase.highest: 
                return new int[4]{Random.Range(minMaxNbDemon[12], minMaxNbDemon[13]),Random.Range(minMaxNbDemon[14], minMaxNbDemon[15]),Random.Range(minMaxNbDemon[16], minMaxNbDemon[17]),Random.Range(minMaxNbDemon[18], minMaxNbDemon[19])};
            
            case WaveManager.Phase.ultimate1:
                return new int[4] { 0, Random.Range(minMaxNbDemon[20], minMaxNbDemon[21]), Random.Range(minMaxNbDemon[22], minMaxNbDemon[23]), Random.Range(minMaxNbDemon[24], minMaxNbDemon[25])};
            
            case WaveManager.Phase.ultimate2:
                return new int[4] { 0, 0, Random.Range(minMaxNbDemon[26], minMaxNbDemon[27]), Random.Range(minMaxNbDemon[28], minMaxNbDemon[29])};

            case WaveManager.Phase.ultimate3:
                return new int[4] { 0, 0, 0, 1 };

            default: return null;
        }
    }

    private int GetNbAllDemonsToSpawn(int[] NbDemonsToSpawn)
    {
        int nb = 0;

        for (int i = 0; i < NbDemonsToSpawn.Length; i++)
        {
            nb += NbDemonsToSpawn[i];
        }
        return nb;
    }

    private int[] SetArrayNbDemonSpawn()
    {
        switch(GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: return new int[1];
            case WaveManager.Phase.mid: return new int[2];
            case WaveManager.Phase.high: return new int[3];
            case WaveManager.Phase.highest: return new int[4];
            case WaveManager.Phase.ultimate1: return new int[4];
            case WaveManager.Phase.ultimate2: return new int[4];
            case WaveManager.Phase.ultimate3: return new int[4];

            default: return null;
        }
    }

    private float GetDelayBetweenSpawn(int nbDemons)
    {
        switch (GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: return timeWaves[0] / nbDemons;
            case WaveManager.Phase.mid: return timeWaves[1] / nbDemons;
            case WaveManager.Phase.high: return timeWaves[2] / nbDemons;
            case WaveManager.Phase.highest: return timeWaves[3] / nbDemons;
            case WaveManager.Phase.ultimate1: return timeWaves[4] / nbDemons;
            case WaveManager.Phase.ultimate2: return timeWaves[5] / nbDemons;
            case WaveManager.Phase.ultimate3: return timeWaves[6] / nbDemons;

            default: return 0;
        }
    }

    private List<int> GetDemonsCanBeSpawn()
    {
        switch(GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: return new List<int>(1) { 0 };
            case WaveManager.Phase.mid: return new List<int>(2) { 0, 1 };
            case WaveManager.Phase.high: return new List<int>(3) { 0, 1, 2 };
            case WaveManager.Phase.highest: return new List<int>(4) { 0, 1, 2, 3 };
            case WaveManager.Phase.ultimate1: return new List<int>(3) { 1, 2, 3 };
            case WaveManager.Phase.ultimate2: return new List<int>(2) { 2, 3 };
            case WaveManager.Phase.ultimate3: return new List<int>(1) { 3 };
            
            default: return null;
        }
    }

    private int GetIndexDemonToSpawn()
    {
        int pourcent = Random.Range(0, 100);

        switch (GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: 
                return 0;

            case WaveManager.Phase.mid:
                return pourcent < 70 ? 0 : 1;

            case WaveManager.Phase.high:
                return pourcent < 55 ? 0 : pourcent < 85 ? 1 : 2;

            case WaveManager.Phase.highest:
                return pourcent < 50 ? 0 : pourcent < 75 ? 1 : pourcent < 90 ? 2 : 3;

            case WaveManager.Phase.ultimate1:
                return pourcent < 55 ? 1 : pourcent < 85 ? 2 : 3;

            case WaveManager.Phase.ultimate2:
                return pourcent < 70 ? 2 : 3;

            case WaveManager.Phase.ultimate3:
                return 3;

            default:return 0;
        }
    }

    private GameObject SpawnDemon(int indexDemon, ref List<int> indexDemonCanBeSpawn, ref int[] nbDemonsToSpawn, ref int[] nbDemonSpawn, ObjectPoolManager.PoolType poolType)
    {
        spawnPoint.x = Random.Range(spawnLimit.x, spawnLimit.y);

        GameObject demon = ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemon], spawnPoint, poolType);
        demon.transform.SetParent(ObjectPoolManager.SetParentObject(demon.GetComponent<DemonStats>().PoolType).transform);
        _demonsSpawn.Add(demon);

        nbDemonSpawn[indexDemon] += 1;
        if (nbDemonSpawn[indexDemon] == nbDemonsToSpawn[indexDemon])
        {
            indexDemonCanBeSpawn.Remove(indexDemon);
        }

        return demon;
    }

    private bool CheckDemonSpawnInScreen()
    {
        foreach (var demon in _demonsSpawn.Where(x => x.activeSelf))
        {
            if (GameManager.Instance.CheckInScreenWithOffset(demon.transform.position))
            { 
                return true; 
            }
        }

        return false;
    }

    public void SpawnBoss()
    {
        _haveToSpawnBoss = false;

        switch(GameManager.Instance.Phase)
        {
            case WaveManager.Phase.low: 
                _indexBossToSpawn = 0;
                break;

            case WaveManager.Phase.mid:
                _indexBossToSpawn = 1;
                break;

            case WaveManager.Phase.high:
                _indexBossToSpawn = 2;
                break;

            case WaveManager.Phase.highest: 
                _indexBossToSpawn = 3;
                break;

        }

        _boss = ObjectPoolManager.SpawnObject(BossPrefab[_indexBossToSpawn], bossSpawnPoint, ObjectPoolManager.PoolType.Boss);
    }

    public void DoSpawnBoss()
    { _haveToSpawnBoss = true; }

    public bool BossIsAlive()
    { return _boss.GetComponent<DemonStats>().Life > 0; }

    public bool IsSpawning { get { return _isSpawning; } }
    public bool DemonInScreen { get { return _demonInScreen; } }
}
