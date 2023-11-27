using UnityEngine;

public class HealthRegeneration : MonoBehaviour
{
    private float _durationRegenration = 2f;
    private float _time = 0;
    private int _nbHpRegene = 10;

    private bool _isRunning = false;

    /*private void OnDisable()
    {
        _isRunning = false;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats player))
        {
            player.LifeRegeneration();
            ObjectPoolManager.ReturnObjectToPool(gameObject);

            /*if (!_isRunning)
            {
                _isRunning = true;
                StartCoroutine(LifeRegene(player));
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }*/
        }
    }

    /*private IEnumerator LifeRegene(PlayerStats player)
    {
        _time = Time.time + _durationRegenration;

        float life = player.Life;
        float maxLife = player.MaxLife;

        while (Time.time < _time) //&& life < maxLife)
        {
            life += _nbHpRegene * Time.deltaTime;
            life = life > maxLife ? maxLife : life;
            player.Stamina = life;
            yield return null;
        }
    }*/
}
