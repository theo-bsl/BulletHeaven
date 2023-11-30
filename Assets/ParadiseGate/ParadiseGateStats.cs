using UnityEngine;

public class ParadiseGateStats : MonoBehaviour
{
    public static ParadiseGateStats Instance;

    private int _maxLife = 1000;
    private int _life = 1000;
    public float Life { get { return _life; } }
    public float MaxLife { get { return _maxLife; } }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TakeDamage(int damage)
    { _life -= damage; }

    public void LifeRegeneration()
    { _life = _maxLife; }
}
