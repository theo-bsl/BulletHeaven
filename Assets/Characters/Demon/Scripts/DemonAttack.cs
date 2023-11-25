using UnityEditorInternal;
using UnityEngine;

public class DemonAttack : MonoBehaviour
{
    private Transform _transform;
    private float _attackTime = 0;
    private int _waitTime = 0;

    public GameObject bulletPrefab;
    public ObjectPoolManager.PoolType poolType;

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
            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position - _transform.up, poolType);
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetDirection(-_transform.up);

            _attackTime = Time.time + _waitTime;
        }
    }
}
