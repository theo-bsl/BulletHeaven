using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float _life = 100;
    private float _maxLife = 100;
    private float _stamina = 100;
    private float _maxStamina = 100;
    private float _damage = 10;
    private float _speed = 15;

    private readonly float _lifeMultiplier = 0.2f;
    private readonly float _staminaMultiplier = 0.2f;
    private readonly float _damageMultiplier = 0.2f;
    private readonly float _xpNeedMultiplier = 0.05f;

    private int _score = 0;
    private float _xp = 0;
    private float _xpNeededToLevelUp = 100;
    private float _level = 1;

    public float Life { get { return _life; } }
    public float Stamina { get { return _stamina; } }
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

    public void LoseStamina(float stamina)
    { _stamina -= stamina;}

    public void TakeScore(int score)
    { _score += score; }

    public void TakeXp(float xp)
    {
        _xp += xp;

        if (_xp >= _xpNeededToLevelUp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        _xp -= _xpNeededToLevelUp;
        _xpNeededToLevelUp += _xpNeededToLevelUp * _xpNeedMultiplier;

        _level += 1;

        _maxLife += _maxLife * _lifeMultiplier;
        _life = _maxLife;

        _maxStamina += _maxStamina * _staminaMultiplier;
        _stamina = _maxStamina;

        _damage += _damage * _damageMultiplier;
    }
}
