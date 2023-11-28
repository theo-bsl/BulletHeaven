using UnityEngine;

public class Bonus : MonoBehaviour
{
    private float _time = 0;
    private float _waitBeforeDeactive = 7;

    private void OnEnable()
    {
        _time = Time.time + _waitBeforeDeactive;
    }

    private void Update()
    {
        if (Time.time > _time)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
