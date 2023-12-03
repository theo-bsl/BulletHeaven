using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rb;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        _rb.velocity = _direction * PlayerStats.Instance.Speed;
    }
}
