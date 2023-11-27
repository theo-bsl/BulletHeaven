using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private Vector2 spawnPoint = Vector2.zero;
    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;
    private float _spawnTime = 20;
    private int _waitSpawn = 20;

    public List<GameObject> bonusPrefabs = new List<GameObject>(5);

    private void Start()
    {
        _minBound = GameManager.Instance.MinBound;
        _minBound.x += GameManager.Instance.BoundOffset;
        _minBound.y += GameManager.Instance.BoundOffset;
        
        _maxBound = GameManager.Instance.MaxBound;
        _maxBound.x -= GameManager.Instance.BoundOffset;
        _maxBound.y -= GameManager.Instance.BoundOffset;
    }

    private void Update()
    {
        if (Time.time > _spawnTime)
        {
            if (Random.Range(0, 100) < 20)
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

            _spawnTime += _waitSpawn;
        }
    }

    private void SetSpawnPoint()
    {
        spawnPoint.x = Random.Range(_minBound.x, _maxBound.x);
        spawnPoint.y = Random.Range(_minBound.y, _maxBound.y);
    }
}
