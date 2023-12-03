using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform _transform;
    private float waitTimer = 0.2f;
    private float bubbleTime = 0;
    private float _nbLoseStamina = 10;
    private bool _isAttacking = false;
    private bool _canAttack = true;
    private bool _autoAttack = false;
    private bool _attackDouble = false;

    public GameObject bulletPrefab;
    public GameObject LaserBeam;

    public enum AttackMode
    {
        Bubble,
        LaserBeam
    }

    public AttackMode attackMode = AttackMode.Bubble;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (_canAttack)
        {
            if (attackMode == AttackMode.Bubble)
            {
                BubbleAttack();
            }
            else if (attackMode == AttackMode.LaserBeam)
            {
                LaserBeamAttack();
            }
        }
    }

    private void BubbleAttack()
    {
        if (_isAttacking || _autoAttack)
        {
            if (bubbleTime < Time.time)
            {
                bubbleTime = Time.time + waitTimer;

                if(_attackDouble)
                {
                    GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position + _transform.up * 3 + _transform.right * 1.5f, ObjectPoolManager.PoolType.PlayerBullet);
                    Bullet bulletComponent = bullet.GetComponent<Bullet>();
                    bulletComponent.SetDirection(_transform.up);
                    bulletComponent.SetDamage(PlayerStats.Instance.Damage);

                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position + _transform.up * 3 - transform.right * 1.5f, ObjectPoolManager.PoolType.PlayerBullet);
                    bulletComponent = bullet.GetComponent<Bullet>();
                    bulletComponent.SetDirection(_transform.up);
                    bulletComponent.SetDamage(PlayerStats.Instance.Damage);
                }
                else
                {
                    GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position + _transform.up * 3, ObjectPoolManager.PoolType.PlayerBullet);
                    Bullet bulletComponent = bullet.GetComponent<Bullet>();
                    bulletComponent.SetDirection(_transform.up);
                    bulletComponent.SetDamage(PlayerStats.Instance.Damage);
                }
            }
        }
    }

    private void LaserBeamAttack()
    {
        if (_isAttacking)
        {
            if (PlayerStats.Instance.Stamina > 0)
            {
                PlayerStats.Instance.LoseStamina(_nbLoseStamina * Time.deltaTime);

                LaserBeam.SetActive(true);
            }
            else
            {
                LaserBeam.SetActive(false);
            }
        }
        else
        {
            LaserBeam.SetActive(false);
        }

        LaserBeam.GetComponent<LaserBeam>().SetAttackDouble(_attackDouble);
    }

    public void SwitchAttackMode()
    {
        attackMode = attackMode == AttackMode.Bubble ? AttackMode.LaserBeam : AttackMode.Bubble;
        LaserBeam.GetComponent<LaserBeam>().Desable();
        _autoAttack = false;
    }

    public void SetIsAttacking(bool isAttacking)
    { _isAttacking = isAttacking;}

    public void SetCanAttack(bool CanAttack)
    { _canAttack = CanAttack; }

    public void SetAttackDouble(bool AttackDouble)
    { _attackDouble = AttackDouble; }

    public void SwitchAutoAttackMode()
    { _autoAttack = !_autoAttack; }
}
