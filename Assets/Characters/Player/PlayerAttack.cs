using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform _transform;
    private float waitTimer = 0.2f;
    private float bubbleTime = 0;
    private bool _isAttacking = false;
    private bool _canAttack = true;

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
        if (_canAttack && _isAttacking)
        {
            if (attackMode == AttackMode.Bubble)
            {
                if (bubbleTime < Time.time)
                {
                    bubbleTime = Time.time + waitTimer;

                    GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, _transform.position + _transform.up * 3, ObjectPoolManager.PoolType.PlayerBullet);
                    Bullet bulletComponent = bullet.GetComponent<Bullet>();
                    bulletComponent.SetDirection(_transform.up);
                    bulletComponent.SetDamage(PlayerStats.Instance.Damage);
                }
            }
            else if (attackMode == AttackMode.LaserBeam)
            {
                LaserBeam.SetActive(true);
            }
        }
        else
        {
            LaserBeam.SetActive(false);
        }
    }

    public void SwitchAttackMode()
    {
        attackMode = attackMode == AttackMode.Bubble ? AttackMode.LaserBeam : AttackMode.Bubble;
        LaserBeam.GetComponent<LaserBeam>().Desable();
    }

    public void SetIsAttacking(bool isAttacking)
    { _isAttacking = isAttacking;}

    public void SetCanAttack(bool CanAttack)
    { _canAttack = CanAttack; }
}
