using System.Collections;
using UnityEngine;

public class DemonBossAttack : MonoBehaviour
{
    private Transform _transform;
    private float _attackTime = 0;
    private float _waitTime = 0;
    private ObjectPoolManager.PoolType poolType;
    private DemonStats.DemonRank rank;
    private bool _isAttacking = false;
    private float _angleToPlayer = 0;

    public GameObject bulletPrefab;
    private GameObject bullet;
    Transform player;
    DemonStats demonStats;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        player = PlayerStats.Instance.transform;
        
        demonStats = _transform.GetComponent<DemonStats>();
        _waitTime = demonStats.TimeBetweenAttacks;
        poolType = demonStats.BulletPoolType;
        rank = demonStats.Rank;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Time.time >= _attackTime && !_isAttacking)
        {
            _isAttacking = true;

            if (rank == DemonStats.DemonRank.Low)
            {
                StartCoroutine(AttackLow());
            }
            else if (rank == DemonStats.DemonRank.Mid)
            {
                StartCoroutine(AttackMid());
            }
            else if (rank == DemonStats.DemonRank.High)
            {
                StartCoroutine(AttackHigh());
            }
            else if (rank == DemonStats.DemonRank.Highest)
            {
                StartCoroutine(AttackHighest());
            }
        }
    }

    private IEnumerator AttackLow()
    {
        for (int i = 0; i < 12; i++)
        {
            bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position, poolType);
            bullet.GetComponent<Bullet>().SetDirection(i * Mathf.PI / 6 * Mathf.Rad2Deg);
        }

        _isAttacking = false;
        _attackTime = Time.time + _waitTime;
        yield return null;
    }

    private IEnumerator AttackMid()
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 360; i += 15)
            {
                bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position, poolType);
                bullet.GetComponent<Bullet>().SetDirection(i);
            }
            yield return new WaitForSeconds(0.25f);
        }

        _isAttacking = false;
        _attackTime = Time.time + _waitTime;
    }

    private IEnumerator AttackHigh()
    {
        for (int k = 0; k < 3; k++)
        {
            for (int i = 0; i < 3; i++)
            {
                _angleToPlayer = Mathf.Atan2(player.position.y - _transform.position.y, player.position.x - _transform.position.x) * Mathf.Rad2Deg;

                for (float j = _angleToPlayer - 45 / 2; j <= _angleToPlayer + 45 / 2; j += 45 / 7)
                {
                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position, poolType);
                    bullet.GetComponent<Bullet>().SetDirection(j);
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        _isAttacking = false;
        yield return null;
    }

    private IEnumerator AttackHighest()
    {
        for (int k = 0; k < 3; k++)
        {
            for (int i = 0; i < 3; i++)
            {
                _angleToPlayer = Mathf.Atan2(player.position.y - _transform.position.y, player.position.x - _transform.position.x) * Mathf.Rad2Deg;

                for (float j = _angleToPlayer - 20 / 2; j <= _angleToPlayer + 20 / 2; j += 20 / 5)
                {
                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position, poolType);
                    bullet.GetComponent<Bullet>().SetDirection(j);
                }
                yield return new WaitForSeconds(0.25f);
            }
            yield return new WaitForSeconds(0.35f);
        }

        _isAttacking = false;
        yield return null;
    }
}
