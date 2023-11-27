using UnityEngine;

public class StaminaBoost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.BoostStamina();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
