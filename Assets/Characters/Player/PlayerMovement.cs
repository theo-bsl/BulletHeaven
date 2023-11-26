using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Move();
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void Move()
    {
        _transform.position += _direction * (PlayerStats.Instance.Speed * Time.deltaTime);
    }
}
