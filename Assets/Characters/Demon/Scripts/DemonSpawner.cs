using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawner : MonoBehaviour
{
    private Vector2 spawnPoint = Vector2.zero;
    private Vector2 spawnLimit = Vector2.zero;
    private float _spawnTime = 0;
    private int _waitSpawn = 7;
    private bool _isSpawning = false;

    public GameObject[] DemonsPrefab = new GameObject[4];

    private float[] timeWaves = new float[4]
    {
        10, //duration of lower-ranking waves
        40, //duration of intermediate-rank waves
        60, //duration of higher-ranking waves
        120  //duration of supreme-ranking waves
    };
    private int[] minMaxNbDemon = new int[20]
    {
        // wave low
        10, 20 ,

        //wave mid
        35, 50 ,
        10, 20 ,

        //wave high
        65, 80 ,
        35, 50 ,
        10, 20 ,

        //wave highest
        95, 110 ,
        65, 80 ,
        35, 50 ,
        10, 20 
    };

    private void Start()
    {
        spawnLimit.x = GameManager.Instance.MinBound.x + 3;
        spawnLimit.y = GameManager.Instance.MaxBound.x - 3;
        spawnPoint.y = GameManager.Instance.MaxBound.y + 6.5f;
    }

    private void Update()
    {
        if (Time.time > _spawnTime && !_isSpawning)
        {
            _isSpawning = true;
            StartCoroutine(SpawnWave());
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

        if (playerScore <= 1000)
        {
            nbDemonsToSpawn = new int[1] { Random.Range(minMaxNbDemon[0], minMaxNbDemon[1]) };
            nbAllDemons = nbDemonsToSpawn[0];
            delayBetweenSpawn = timeWaves[0] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                spawnPoint.x = Random.Range(spawnLimit.x,spawnLimit.y);

                ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemonCanBeSpawn[0]], spawnPoint, ObjectPoolManager.PoolType.DemonLow);

                yield return new WaitForSecondsRealtime(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (playerScore <= 5000)
        {
            nbDemonsToSpawn = new int[2] { Random.Range(minMaxNbDemon[2], minMaxNbDemon[3]), Random.Range(minMaxNbDemon[4], minMaxNbDemon[5]) };
            nbAllDemons = nbDemonsToSpawn[0] + nbDemonsToSpawn[1];
            nbDemonSpawn = new int[2];
            delayBetweenSpawn = timeWaves[1] / nbAllDemons;
            indexDemonCanBeSpawn = new List<int>(1) { 0, 1 };

            for (int i = 0; i < nbAllDemons; i++)
            {
                indexDemon = Random.Range(0, 100) < 70 ? 0 : 1;
                SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonMid);
                yield return new WaitForSecondsRealtime(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (playerScore <= 15000)
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
                SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonHigh);
                yield return new WaitForSecondsRealtime(delayBetweenSpawn);
            }
            _spawnTime = Time.time + _waitSpawn;
        }
        else if (playerScore <= 33000)
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
                SpawnDemon(indexDemon, ref indexDemonCanBeSpawn, ref nbDemonsToSpawn, ref nbDemonSpawn, ObjectPoolManager.PoolType.DemonHighest);
                yield return new WaitForSecondsRealtime(delayBetweenSpawn);
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

    private void SpawnDemon(int indexDemon, ref List<int> indexDemonCanBeSpawn, ref int[] nbDemonsToSpawn, ref int[] nbDemonSpawn, ObjectPoolManager.PoolType poolType)
    {
        spawnPoint.x = Random.Range(spawnLimit.x, spawnLimit.y);

        ObjectPoolManager.SpawnObject(DemonsPrefab[indexDemon], spawnPoint, poolType);

        nbDemonSpawn[indexDemon] += 1;
        if (nbDemonSpawn[indexDemon] == nbDemonsToSpawn[indexDemon])
        {
            indexDemonCanBeSpawn.Remove(indexDemon);
        }
    }
}
