using UnityEngine;

public class XpBoost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.BoostXp();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
