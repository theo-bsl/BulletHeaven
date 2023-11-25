using UnityEngine;

public class DemonBullet : Bullet
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.TakeDamage(_damage);

            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
