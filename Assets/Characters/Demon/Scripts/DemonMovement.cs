using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _direction = Vector3.down;
    private float _speed = 1;
    private int _damage = 1;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _speed = GetComponent<DemonStats>().Speed;
        _damage = GetComponent<DemonStats>().Score;
    }

    private void Update()
    {
        Move();

        if (!GameManager.Instance.CheckInScreenWithOffset(_transform.position))
        {
            GetComponent<DemonStats>().ResetLife();
            ParadiseGateStats.Instance.TakeDamage(_damage);
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    public void Move()
    {
        _transform.position += _direction * (_speed * Time.deltaTime);
    }
}
