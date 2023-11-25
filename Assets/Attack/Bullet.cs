using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    public float _speed = 1;
    public float _damage = 1;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 10);
    }

    private void Update()
    {
        Move();

        if (GameManager.Instance.CheckInScreen(_transform.position))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void Move()
    {
        _transform.position += _direction * _speed * Time.deltaTime;
    }

    public void SetDirection(Vector3 direction)
    { _direction = direction; }
    public void SetDamage(float damage)
    { _damage = damage; }
}
