using UnityEngine;

public class DoubleAttackBonus : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.AttackDouble();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
