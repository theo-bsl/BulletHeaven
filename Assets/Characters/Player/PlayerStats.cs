using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float _life = 100;
    private float _damage = 0;
    private float _speed = 15;

    private int _score = 0;
    private float _xp = 0;
    private float _level = 1;

    public float Life { get { return _life; } }
    public float Damage { get { return _damage; } }
    public float Speed { get { return _speed; } }
    
    public float Score { get { return _score; } }
    public float XP { get { return _xp; } }
    public float Level { get { return _level; } }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TakeDamage(float damage)
    { _life -= damage; _life = _life < 0 ? 0 : _life; }
}
