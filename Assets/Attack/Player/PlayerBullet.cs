using UnityEngine;

public class PlayerBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DemonStats demon))
        {
            demon.TakeDamage(_damage);

            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}