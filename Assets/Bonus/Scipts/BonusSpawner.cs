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

    public List<GameObject> bonusPrefabs = new List<GameObject>(5);

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
                if (Random.Range(0, 100) < 50)
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
        var phase = GameManager.Instance.Phase;

        if (phase == WaveManager.Phase.mid)
        {
            ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 1 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
        }
        else if (phase == WaveManager.Phase.high)
        {
            ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 3 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
        }
        else if (phase >= WaveManager.Phase.highest)
        {
            ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 4 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
        }
    }

    private void SetSpawnPoint()
    {
        spawnPoint.x = Random.Range(_minBound.x, _maxBound.x);
        spawnPoint.y = Random.Range(_minBound.y, _maxBound.y);
    }

    public bool CanSpawn {  get { return _canSpawn; } set { _canSpawn = value; } }
}
