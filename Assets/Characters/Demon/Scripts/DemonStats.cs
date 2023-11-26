using UnityEngine;

public class DemonStats : MonoBehaviour
{
    public float Life = 100;
    public float MaxLife = 100;
    public int TimeBetweenAttacks = 0;
    public float Speed = 15;

    public float XP = 0;
    public int Score = 0;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-10, 0);
    }

    public void TakeDamage(float damage)
    {
        Life -= damage;

        if (Life <= 0 )
        {
            Death();
        }
    }

    public void ResetLife()
    { Life = MaxLife; }

    private void Death()
    {
        PlayerStats.Instance.TakeScore(Score);
        PlayerStats.Instance.TakeXp(XP);
        PlayerStats.Instance.IncreaseNbKill();

        Life = MaxLife;

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
