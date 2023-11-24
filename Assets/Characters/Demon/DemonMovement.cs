using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _direction = Vector3.down;
    private readonly int _speed = 15;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Move();

        if (GameManager.Instance.CheckInScreen(_transform.position))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    public void Move()
    {
        _transform.position += _direction * (_speed * Time.deltaTime);
    }
}
