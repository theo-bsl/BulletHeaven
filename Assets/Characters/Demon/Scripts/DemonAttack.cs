using UnityEngine;

public class DemonAttack : MonoBehaviour
{
    private Transform _transform;
    private float _attackTime = 0;
    private int _waitTime = 0;

    public GameObject bulletPrefab;
    public ObjectPoolManager.PoolType poolType;

    private GameObject bullet;

    public enum DemonRank
    {
        Low, Mid, High, Highest , EasterEgg
    }
    public DemonRank rank;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        DemonStats demon = _transform.GetComponent<DemonStats>();

        _waitTime = demon.TimeBetweenAttacks;
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Time.time > _attackTime)
        {
            /*GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
            bullet.GetComponent<Bullet>().SetDirection(-_transform.up);*/

            if (rank == DemonRank.Low)
            {
                bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
                bullet.GetComponent<Bullet>().SetDirection(-_transform.up);
            }
            else if (rank == DemonRank.Mid)
            {
                bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up + _transform.right, poolType);
                bullet.GetComponent<Bullet>().SetDirection(-_transform.up);

                bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up - _transform.right, poolType);
                bullet.GetComponent<Bullet>().SetDirection(-_transform.up);
            }
            else if (rank == DemonRank.High)
            {
                for (float i = 157.5f + 90; i <= 202.5f + 90; i += 15)
                {
                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
                    bullet.GetComponent<Bullet>().SetDirection(i);
                }
            }
            else if (rank == DemonRank.Highest)
            {
                for (float i = 135 + 90; i <= 225 + 90; i += 22.5f /*15*/)
                {
                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
                    bullet.GetComponent<Bullet>().SetDirection(i);
                }
            }
            else if (rank == DemonRank.EasterEgg)
            {
                for (int i = 0; i <= 360; i += 30)
                {
                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
                    bullet.GetComponent<Bullet>().SetDirection(i);
                }
            }

            _attackTime = Time.time + _waitTime;
        }
    }
}
