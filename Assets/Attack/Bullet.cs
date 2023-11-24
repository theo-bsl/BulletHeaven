using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    public float _speed = 100;
    public float _damage = 1;
    private float _offset = 10;
    private Vector3 _direction = Vector3.zero;
    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _minBound = GameManager.Instance.MinBound;
        _maxBound = GameManager.Instance.MaxBound;
    }

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Random.Range(0, 10);
    }

    private void Update()
    {
        Move();

        if (CheckInScreen())
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

    public void SetSpeed(float speed)
    { _speed = speed; }

    public void SetBound(Vector2 minBound, Vector2 maxBound)
    {
        _minBound = minBound;
        _maxBound = maxBound;
    }

    private bool CheckInScreen()
    {
        if (_transform.position.x > _maxBound.x + _offset)
        {
            return true;
        }
        else if (_transform.position.x < _minBound.x - _offset)
        {
            return true;
        }
        else if (_transform.position.y > _maxBound.y + _offset)
        {
            return true;
        }
        else if (_transform.position.y < _minBound.y - _offset)
        {
            return true;
        }
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("bullet touched an enemy");
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        /*else if (collision.gameObject.CompareTag("WorldBorder"))
        {

        }*/
    }
}
