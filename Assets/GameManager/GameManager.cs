using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Vector2 _minBound = Vector2.zero;
    private Vector2 _maxBound = Vector2.zero;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        _minBound = Camera.main.ViewportToWorldPoint(Vector2.zero);
        _maxBound = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    public Vector2 MinBound { get { return _minBound; } }
    public Vector2 MaxBound { get { return _maxBound; } }
}
