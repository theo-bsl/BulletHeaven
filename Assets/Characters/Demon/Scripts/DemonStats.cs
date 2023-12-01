using UnityEngine;

public class DemonStats : MonoBehaviour
{
    public float Life = 100;
    public float MaxLife = 100;
    public float TimeBetweenAttacks = 0;
    public float Speed = 15;
    public int Damage = 0;

    public float XP = 0;
    public int Score = 0;

    public ObjectPoolManager.PoolType PoolType = ObjectPoolManager.PoolType.DemonLow;
    public ObjectPoolManager.PoolType BulletPoolType = ObjectPoolManager.PoolType.DemonLow;

    public enum DemonRank
    {
        Low, Mid, High, Highest, EasterEgg
    }
    public DemonRank _rank = DemonRank.Low;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Random.Range(-10, 0);
        ResetLife();
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

        if (PoolType != ObjectPoolManager.PoolType.Boss)
        {
            GetComponent<DemonAttack>().Death();
        }

        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerStats player))
        {
            player.TakeDamage(Damage * Time.deltaTime);
        }
    }

    public DemonRank Rank { get { return _rank; } }
}
