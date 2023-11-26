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
    }

    private void Start()
    {
        _rayDistance = Mathf.Abs(GameManager.Instance.MinBound.x) + GameManager.Instance.MaxBound.x + _offsetRay;
    }

    private void Update()
    {
        if (PlayerStats.Instance.Stamina > 0)
        {
            PlayerStats.Instance.LoseStamina(10 * Time.deltaTime);

            var colliders = Physics2D.RaycastAll(BubbleLaserBeam.transform.position, _transform.up, _rayDistance, _enemyLayer | _worldBorderLayer);

            if (colliders.Length > 0)
            {
                RaycastHit2D hit = colliders[0];

                _scale.y = Vector2.Distance(BubbleLaserBeam.transform.position, hit.transform.position);
                _transform.localScale = _scale;

                _transform.position = Vector2.Lerp(BubbleLaserBeam.transform.position, hit.point, 0.5f);

                if (hit.transform.TryGetComponent(out DemonStats demon))
                {
                    demon.TakeDamage(PlayerStats.Instance.Damage * 6 * Time.deltaTime);
                }
            }
            else
            {
                Debug.LogWarning("Laser beam collide with nothing");
            }
        }
    }

    public void Desable()
    {
        BubbleLaserBeam.SetActive(false);
        gameObject.SetActive(false);
    }
}
