using UnityEngine;

public class DemonBossMovement : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _destination = Vector3.zero;
    private float _speed = 1;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _speed = GetComponent<DemonStats>().Speed;
    }

    private void Update()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _destination, _speed * Time.deltaTime);
    }
}
