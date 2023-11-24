using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _transform;
    public float _speed = 100;
    public float _damage = 1;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _damage = PlayerStats.Instance.Damage;
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

    public void SetSpeed(float speed)
    { _speed = speed; }

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
