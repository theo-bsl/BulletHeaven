using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    private float _maxLife = 50;
    private float _maxStamina = 50;

    private float _life = 50;
    private float _stamina = 50;
    private float _damage = 5;
    private float _speed = 15;
    private int _score = 0;
    private int _nbKill = 0;
    private float _xp = 0;
    private float _level = 1;

    private float _lifeAdd = 5;
    private float _staminaAdd = 5;
    private float _damageAdd = 2.5f;
    private float _xpNeedAdd = 15;
    private float _levelUpDecreaser = 0.90f;
    private float _xpNeedAddMultiplier = 0.2f;
    private int _levelToDecreaseLevelUp = 2;
    private float _xpNeededToLevelUp = 100;

    private bool _canLoseStamina = true;
    private int _durationXpBoost = 3;
    private float _durationStaminaBoost = 3;
    private int _xpBoost = 1;
    private int _durationAttackDouble = 20;

    private int _laserBeamDamageMultiplier = 10;

    public List<Sprite> Skins = new List<Sprite>(3);
    private int indexSkins = 0;

    #region Get
    public float Life { get { return _life; } }
    public float MaxLife { get { return _maxLife; } }
    public float Stamina { get { return _stamina; } }
    public float MaxStamina { get { return _maxStamina; } }
    public float Damage { get { return _damage; } }
    public float Speed { get { return _speed; } }
    

    public float Score { get { return _score; } }
    public int NbKill { get { return _nbKill; } }
    public float XP { get { return _xp; } }
    public float XpNeededToLevelUp { get {  return _xpNeededToLevelUp; } }
    public float Level { get { return _level; } }


    public int LaserBeamDamageMultiplier { get { return _laserBeamDamageMultiplier; } }
    #endregion


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TakeDamage(float damage)
    { _life -= damage; _life = _life < 0 ? 0 : _life; }

    public void LoseStamina(float stamina)
    {
        if (_canLoseStamina)
        {
            _stamina -= stamina;
            _stamina = _stamina < 0 ? 0 : _stamina;
        }
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
        _xpNeededToLevelUp += _xpNeedAdd;

        _level += 1;

        bool lifeIsMax = _life == _maxLife;
        _maxLife += _lifeAdd;
        _life += lifeIsMax ? _maxLife - _life : (_maxLife - _life) / 2;

        bool staminaIsMax = _stamina == _maxStamina;
        _maxStamina += _staminaAdd;
        _stamina += staminaIsMax ? _maxStamina - _stamina : (_maxStamina - _stamina) / 2;

        _damage += _damageAdd;

        if (_level % _levelToDecreaseLevelUp == 0)
        {
            DecreaseLevelUp();
        }

        if (_level % 10 == 0)
        {
            UpgradePlayer();
        }
    }

    private void DecreaseLevelUp()
    {
        _damageAdd *= _levelUpDecreaser;
        _lifeAdd *= _levelUpDecreaser;
        _staminaAdd *= _levelUpDecreaser;

        _xpNeedAdd += _xpNeedAdd * _xpNeedAddMultiplier;
    }

    private void UpgradePlayer()
    {
        GetComponent<SpriteRenderer>().sprite = Skins[indexSkins++];
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
        _stamina = _maxStamina;

        yield return new WaitForSeconds(_durationStaminaBoost);

        _canLoseStamina = true;
    }

    public void AttackDouble()
    {
        StartCoroutine(AttackDoubled());
    }

    private IEnumerator AttackDoubled()
    {
        GetComponent<PlayerAttack>().SetAttackDouble(true);

        yield return new WaitForSeconds(_durationAttackDouble);

        GetComponent<PlayerAttack>().SetAttackDouble(false);
    }
}
