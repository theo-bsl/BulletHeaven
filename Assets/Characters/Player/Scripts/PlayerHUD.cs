using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD Instance;

    private PlayerStats _playerStats;

    public Image life;
    public Image stamina;
    public Image xp;
    public TextMeshProUGUI level;

    public TextMeshProUGUI time;
    public TextMeshProUGUI score;
    public TextMeshProUGUI kill;

    public GameObject Bubble;
    public GameObject LaserBeam;

    public TextMeshProUGUI maxLife;
    private string s_maxlife;
    public TextMeshProUGUI maxStamina;
    private string s_maxstamina;
    public TextMeshProUGUI maxXp;
    private string s_maxxp;
    public TextMeshProUGUI damage;
    private string s_maxdamage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        s_maxlife = maxLife.text;
        s_maxstamina = maxStamina.text;
        s_maxxp = maxXp.text;
        s_maxdamage = damage.text;
    }

    private void Start()
    {
        _playerStats = PlayerStats.Instance;
    }

    private void Update()
    {
        life.fillAmount = _playerStats.Life / _playerStats.MaxLife;
        stamina.fillAmount = _playerStats.Stamina / _playerStats.MaxStamina;
        xp.fillAmount = _playerStats.XP / _playerStats.XpNeededToLevelUp;

        level.text = _playerStats.Level.ToString();

        time.text = ((int)GameManager.Instance.AllTime).ToString();
        score.text = _playerStats.Score.ToString();
        kill.text = _playerStats.NbKill.ToString();

        //--------------------------------------

        maxLife.text = s_maxlife + _playerStats.MaxLife.ToString();
        maxStamina.text = s_maxstamina + _playerStats.MaxStamina.ToString();
        maxXp.text = s_maxxp + _playerStats.XpNeededToLevelUp.ToString();
        damage.text = s_maxdamage + _playerStats.Damage.ToString();
    }

    public void SwitchAttackMode()
    {
        Bubble.SetActive(!Bubble.activeSelf);
        LaserBeam.SetActive(!LaserBeam.activeSelf);
    }
}
