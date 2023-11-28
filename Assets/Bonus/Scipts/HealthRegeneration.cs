using UnityEngine;

public class HealthRegeneration : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.LifeRegeneration();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
