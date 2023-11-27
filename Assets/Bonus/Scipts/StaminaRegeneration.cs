using UnityEngine;

public class StaminaRegeneration : MonoBehaviour
{
    private float _durationRegenration = 2f;
    private float _time = 0;
    private int _nbSpRegene = 10;

    private bool _isRunning = false;

    /*private void OnDisable()
    {
        _isRunning = false;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.StaminaRegeneration();
            ObjectPoolManager.ReturnObjectToPool(gameObject);

            /*if (!_isRunning)
            {
                _isRunning = true;
                StartCoroutine(StaminaRegene(player));
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }*/
        }
    }

    /*private IEnumerator StaminaRegene(PlayerStats player)
    {
        _time = Time.time + _durationRegenration;

        float stamina = player.Stamina;
        float maxStamina = player.MaxStamina;

        while (Time.time < _time) //&& stamina < maxStamina)
        {
            stamina += _nbSpRegene * Time.deltaTime;
            stamina = stamina > maxStamina ? maxStamina : stamina;
            player.Stamina = stamina;
            yield return null;
        }
    }*/
}
