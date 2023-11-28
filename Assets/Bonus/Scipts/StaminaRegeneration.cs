using UnityEngine;

public class StaminaRegeneration : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.StaminaRegeneration();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
