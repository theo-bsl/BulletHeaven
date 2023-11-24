using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private Transform _transform;
    private bool _isRotating = false;
    private float _direction = 0;
    private float _activationTime = 0;
    private float _waitActivationTime = 0.5f;
    private float _rotationTime = 0;
    private float _waitRotationTime = 0.5f;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_direction != 0)
        {
            if (!_isRotating)
            {
                _isRotating = true;
                _activationTime = Time.time + _waitActivationTime;
                RotateKeyboard(_direction);
            }
            else
            {
                if (Time.time > _activationTime)
                {
                    if (Time.time > _rotationTime)
                    {
                        RotateKeyboard(_direction);
                        _rotationTime = Time.time + _waitRotationTime;
                    }
                }
            }
        }
        else
        {
            _isRotating = false;
        }
    }

    public void RotateKeyboard(float direction)
    {
        _transform.rotation = Quaternion.Euler(0, 0, _transform.eulerAngles.z + 90 * direction);
    }

    public void SetRotation(float direction)
    {
        _direction = direction;
    }

    public void RotateGamepad(Vector2 direction)
    {
        if (direction.x == 1 || direction.x == -1)
            _transform.rotation = Quaternion.Euler(0, 0, 90 * direction.x);
        else if (direction.y == 1)
            _transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (direction.y == -1)
            _transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
