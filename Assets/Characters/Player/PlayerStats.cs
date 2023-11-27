using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float _maxLife = 100;
    private float _maxStamina = 100;
    private int _durationXpBoost = 3;

    private float _life = 100;
    private float _stamina = 100;
    private float _damage = 10;
    private float _speed = 15;
    private int _score = 0;
    private int _nbKill = 0;
    private float _xp = 0;
    private float _level = 1;

    private readonly float _lifeMultiplier = 0.2f;
    private readonly float _staminaMultiplier = 0.2f;
    private readonly float _damageMultiplier = 0.2f;
    private readonly float _xpNeedMultiplier = 0.05f;

    private bool _canLoseStamina = true;
    private float _durationStaminaBoost = 3;
    private int _xpBoost = 1;

    private float _xpNeededToLevelUp = 100;

    private float _startTime = 0;

    #region Get
    public float Life { get { return _life; } }
    public float MaxLife { get { return _maxLife; } }
    public float Stamina { get { return _stamina; } }
    public float MaxStamina { get { return _maxStamina; } }
    public float Damage { get { return _damage; } }
    public float Speed { get { return _speed; } }
    
    public float StartTime { get { return _startTime; } }
    public float Score { get { return _score; } }
    public int NbKill { get { return _nbKill; } }
    public float XP { get { return _xp; } }
    public float XpNeededToLevelUp { get {  return _xpNeededToLevelUp; } }
    public float Level { get { return _level; } }
    #endregion


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _startTime = Time.time;
    }

    public void TakeDamage(float damage)
    { _life -= damage; _life = _life < 0 ? 0 : _life; }

    public void LoseStamina(float stamina)
    {
        if (_canLoseStamina)
            _stamina -= stamina;
    }

    public void TakeScore(int score)
    { _score += score; }

    public void TakeXp(float xp)
    {
        _xp += xp * _xpBoost;

        if (_xp >= _xpNeededToLevelUp)
        {
            LevelUp();
        }
    }

    public void IncreaseNbKill()
    { _nbKill += 1; }

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

    public void StopAttack()
    { GetComponent<PlayerAttack>().SetCanAttack(false); }

    public void StartAttack()
    { GetComponent<PlayerAttack>().SetCanAttack(true); }

    public void LifeRegeneration()
    { _life = _maxLife; }

    public void StaminaRegeneration()
    { _stamina = _maxStamina; }

    public void BoostXp()
    {
        StartCoroutine(XpBoosted());
    }

    private IEnumerator XpBoosted()
    {
        _xpBoost = 3;

        yield return new WaitForSeconds(_durationXpBoost);

        _xpBoost = 1;
    }

    public void BoostStamina()
    {
        StartCoroutine(StaminaBoosted());
    }

    private IEnumerator StaminaBoosted()
    {
        _canLoseStamina = false;

        yield return new WaitForSeconds(_durationStaminaBoost);

        _canLoseStamina = true;
    }
}
