using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    private Transform _transform;
    public GameObject BubbleLaserBeam;
    private Vector2 _scale = Vector2.zero;
    private LayerMask _enemyLayer;
    private LayerMask _worldBorderLayer;
    private float _rayDistance = 0;
    private float _offsetRay = 10;
    private int _laserBeamDamageMultiplier = 1;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _bubbleSpriteRenderer;

    private void OnEnable()
    {
        BubbleLaserBeam.SetActive(true);
    }
    private void OnDisable()
    {
        BubbleLaserBeam.SetActive(false);
    }

    private void Awake()
    {
        _transform = transform;
        _scale = _transform.localScale;
        _enemyLayer = LayerMask.GetMask("Enemy");
        _worldBorderLayer = LayerMask.GetMask("WorldBorder");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _bubbleSpriteRenderer = BubbleLaserBeam.GetComponent<SpriteRenderer>();

        gameObject.SetActive(false);
    }

    private void Start()
    {
        _rayDistance = Mathf.Abs(GameManager.Instance.MinBound.x) + GameManager.Instance.MaxBound.x + _offsetRay;
    }

    private void Update()
    {
        SetLaserBeam();
        AttackEnemies();
    }

    private void SetLaserBeam()
    {
        var colliders = Physics2D.RaycastAll(BubbleLaserBeam.transform.position, _transform.up, _rayDistance, _worldBorderLayer);

        if (colliders.Length > 0)
        {
            RaycastHit2D hit = colliders[0];

            _scale.y = Vector2.Distance(BubbleLaserBeam.transform.position, hit.point);
            _transform.localScale = _scale;

            _transform.position = Vector2.Lerp(BubbleLaserBeam.transform.position, hit.point, 0.5f);
        }
        else
        {
            Debug.LogWarning("Laser beam collide with nothing");
        }
    }

    private void AttackEnemies()
    {
        var colliders = Physics2D.RaycastAll(BubbleLaserBeam.transform.position, _transform.up, _rayDistance, _enemyLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.TryGetComponent(out DemonStats demon))
            {
                demon.TakeDamage(PlayerStats.Instance.Damage * _laserBeamDamageMultiplier * Time.deltaTime);
            }
        }
    }

    public void SetAttackDouble(bool AttackDouble)
    {
        if (AttackDouble)
        {
            _laserBeamDamageMultiplier = PlayerStats.Instance.LaserBeamDamageMultiplier * 2;
            _spriteRenderer.color = Color.red;
            _bubbleSpriteRenderer.color = Color.red;
        }
        else
        {
            _laserBeamDamageMultiplier = PlayerStats.Instance.LaserBeamDamageMultiplier;
            _spriteRenderer.color = Color.cyan;
            _bubbleSpriteRenderer.color = Color.cyan;
        }
    }

    public void Desable()
    {
        BubbleLaserBeam.SetActive(false);
        gameObject.SetActive(false);
    }
}
