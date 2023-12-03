using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public static BonusSpawner Instance;

    private Vector2 spawnPoint = Vector2.zero;
    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;
    private float _spawnTime = 0;
    private int _waitSpawn = 10;
    private bool _canSpawn = false;

    public List<GameObject> bonusPrefabs = new List<GameObject>(6);
    private int[] indexBonusCanBeSpawn = new int[6];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        int boundOffset = GameManager.Instance.BoundOffset;

        _minBound = GameManager.Instance.MinBound;
        _minBound.x += boundOffset / 2f;
        _minBound.y += boundOffset;
        
        _maxBound = GameManager.Instance.MaxBound;
        _maxBound.x -= boundOffset / 2;

        float distanceY = _maxBound.y - _minBound.y;
        float heaven = distanceY * 70 / 100;
        float transition = distanceY * 15 / 100;
        _maxBound.y = _minBound.y + heaven - transition / 2f - boundOffset;
    }

    private void Update()
    {
        if (_canSpawn)
        {
            if (Time.time > _spawnTime)
            {
                //if (Random.Range(0, 100) < 50)
                {
                    SetSpawnPoint();
                    SpawnBonus();
                }

                _spawnTime = Time.time + _waitSpawn;
            }
        }
    }

    private void SpawnBonus()
    {
        if (GameManager.Instance.Phase > WaveManager.Phase.low)
        {
            ObjectPoolManager.SpawnObject(bonusPrefabs[ChooseRandomBonus()], spawnPoint, ObjectPoolManager.PoolType.Bonus);
        }
    }

    private void SetSpawnPoint()
    {
        spawnPoint.x = Random.Range(_minBound.x, _maxBound.x);
        spawnPoint.y = Random.Range(_minBound.y, _maxBound.y);
    }

    public void UpdateBonusCanBeSpawn()
    {
        switch(GameManager.Instance.Phase)
        {
            case WaveManager.Phase.mid: indexBonusCanBeSpawn = new int[2] { 0, 1 }; break;
            case WaveManager.Phase.high: indexBonusCanBeSpawn = new int[4] { 0, 1, 2, 3 }; break;
            case > WaveManager.Phase.high: indexBonusCanBeSpawn = new int[6] { 0, 1, 2, 3, 4, 5 }; break;
        }
    }

    private int ChooseRandomBonus()
    {
        return indexBonusCanBeSpawn[Random.Range(0, indexBonusCanBeSpawn.Length)];
    }

    public bool CanSpawn {  get { return _canSpawn; } set { _canSpawn = value; _spawnTime = Time.time + _waitSpawn; } }
}
