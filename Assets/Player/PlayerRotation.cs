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
                Rotate(_direction);
            }
            else
            {
                if (Time.time > _activationTime)
                {
                    if (Time.time > _rotationTime)
                    {
                        Rotate(_direction);
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

    public void Rotate(float direction)
    {
        _transform.rotation = Quaternion.Euler(0, 0, _transform.eulerAngles.z + 90 * direction);
    }

    public void SetRotation(float direction)
    {
        _direction = direction;
    }
}
