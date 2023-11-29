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
        float delayBetweenSpawn = 0;
        int[] nbDemonsToSpawn = null;
        int nbAllDemons = 0;
        int[] nbDemonSpawn = null;
        List<int> indexDemonCanBeSpawn = null;
        int indexDemon = 0;

        if (GameManager.Instance.Phase == WaveManager.Phase.low)
        {
            nbDemonsToSpawn = new int[1] { Random.Range(minMaxNbDemon[0], minMaxNbDemon[1]) };
            nbAllDemons = nbDemonsToSpawn[0];
            delayBetweenSpawn = timeWaves[0] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                spawnPoint.x = Random.Range(spawnLimit.x, spawnLimit.y);

                _demonsSpawn.Add(ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemonCanBeSpawn[0]], spawnPoint, ObjectPoolManager.PoolType.DemonLow));

                yield return new WaitForSeconds(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (GameManager.Instance.Phase == WaveManager.Phase.mid)
        {
            nbDemonsToSpawn = new int[2] { Random.Range(minMaxNbDemon[2], minMaxNbDemon[3]), Random.Range(minMaxNbDemon[4], minMaxNbDemon[5]) };
            nbAllDemons = nbDemonsToSpawn[0] + nbDemonsToSpawn[1];
            nbDemonSpawn = new int[2];
            delayBetweenSpawn = timeWaves[1] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0, 1 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                indexDemon = Random.Range(0, 100) < 70 ? 0 : 1;
                _demonsSpawn.Add(SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonMid));
                yield return new WaitForSeconds(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (GameManager.Instance.Phase == WaveManager.Phase.high)
        {
            nbDemonsToSpawn = new int[3]
            {
                Random.Range(minMaxNbDemon[6], minMaxNbDemon[7]),
                Random.Range(minMaxNbDemon[8], minMaxNbDemon[9]),
                Random.Range(minMaxNbDemon[10], minMaxNbDemon[11])
            };
            nbAllDemons = nbDemonsToSpawn[0] + nbDemonsToSpawn[1] + nbDemonsToSpawn[2];
            nbDemonSpawn = new int[3];
            delayBetweenSpawn = timeWaves[2] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0, 1, 2 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                int pourcent = Random.Range(0, 100);
                indexDemon = pourcent < 55 ? 0 : pourcent < 85 ? 1 : 2;
                _demonsSpawn.Add(SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonHigh));
                yield return new WaitForSeconds(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (GameManager.Instance.Phase == WaveManager.Phase.highest)
        {
            nbDemonsToSpawn = new int[4]
            {
                Random.Range(minMaxNbDemon[12], minMaxNbDemon[13]),
                Random.Range(minMaxNbDemon[14], minMaxNbDemon[15]),
                Random.Range(minMaxNbDemon[16], minMaxNbDemon[17]),
                Random.Range(minMaxNbDemon[18], minMaxNbDemon[19])
            };
            nbAllDemons = nbDemonsToSpawn[0] + nbDemonsToSpawn[1] + nbDemonsToSpawn[2] + nbDemonsToSpawn[3];
            nbDemonSpawn = new int[4];
            delayBetweenSpawn = timeWaves[3] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0, 1, 2, 3 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                int pourcent = Random.Range(0, 100);
                indexDemon = pourcent < 50 ? 0 : pourcent < 75 ? 1 : pourcent < 90 ? 2 : 3;
                _demonsSpawn.Add(SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonHighest));
                yield return new WaitForSeconds(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else
        {
            spawnPoint.x = Random.Range(spawnLimit.x, spawnLimit.y);

            //int indexDemon = Random.Range(2, 3 + 1);
            //int indexDemon = Random.value < 0.6f ? 2 : 3;
            indexDemon = Random.Range(0, 100) < 66 ? 2 : 3;

            ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemon], spawnPoint, ObjectPoolManager.PoolType.DemonLow);

            _spawnTime = Time.time + 0.5f;
        }

        _isSpawning = false;
    }

    private GameObject SpawnDemon(int indexDemon, ref List<int> indexDemonCanBeSpawn, ref int[] nbDemonsToSpawn, ref int[] nbDemonSpawn, ObjectPoolManager.PoolType poolType)
    {
        spawnPoint.x = Random.Range(spawnLimit.x, spawnLimit.y);

        GameObject demon = ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemon], spawnPoint, poolType);

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
            if (GameManager.Instance.CheckInScreen(demon.transform.position))
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

    private void SpawnRandomBoss()
    {
        _boss = ObjectPoolManager.SpawnObject(BossPrefab[Random.Range(0, BossPrefab.Length)], bossSpawnPoint, ObjectPoolManager.PoolType.Boss);
    }

    public void DoSpawnBoss()
    { _haveToSpawnBoss = true; }

    public bool BossIsAlive()
    {
        return _boss.GetComponent<DemonStats>().Life > 0;
    }

    public bool IsSpawning { get { return _isSpawning; } }
    public bool DemonInScreen { get { return _demonInScreen; } }
}
