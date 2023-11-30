using UnityEngine;

public class GateRegeneration : Bonus
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            ParadiseGateStats.Instance.LifeRegeneration();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
