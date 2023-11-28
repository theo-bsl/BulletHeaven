using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private Vector2 spawnPoint = Vector2.zero;
    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;
    private float _spawnTime = 0;
    private int _waitSpawn = 10;

    public List<GameObject> bonusPrefabs = new List<GameObject>(5);

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
        if (Time.time > _spawnTime)
        {
            if (Random.Range(0, 100) < 50)
            {
                float playerScore = PlayerStats.Instance.Score;
                SetSpawnPoint();

                if (WaveManager.MaxScoreWave1 < playerScore && playerScore < WaveManager.MaxScoreWave2)
                {
                    ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 1 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
                }
                else if (WaveManager.MaxScoreWave2 < playerScore && playerScore < WaveManager.MaxScoreWave3)
                {
                    ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 3 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
                }
                else if (WaveManager.MaxScoreWave3 < playerScore)
                {
                    ObjectPoolManager.SpawnObject(bonusPrefabs[Random.Range(0, 4 + 1)], spawnPoint, ObjectPoolManager.PoolType.Bonus);
                }
            }

            _spawnTime = Time.time + _waitSpawn;
        }
    }

    private void SetSpawnPoint()
    {
        spawnPoint.x = Random.Range(_minBound.x, _maxBound.x);
        spawnPoint.y = Random.Range(_minBound.y, _maxBound.y);
    }
}
