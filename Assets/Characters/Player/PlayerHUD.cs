using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    private PlayerStats playerStats;

    public Image life;
    public Image stamina;
    public Image xp;
    public TextMeshProUGUI level;

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
        playerStats = GetComponent<PlayerStats>();

        s_maxlife = maxLife.text;
        s_maxstamina = maxStamina.text;
        s_maxxp = maxXp.text;
        s_maxdamage = damage.text;
    }

    private void Update()
    {
        life.fillAmount = playerStats.Life / playerStats.MaxLife;
        stamina.fillAmount = playerStats.Stamina / playerStats.MaxStamina;
        xp.fillAmount = playerStats.XP / playerStats.XpNeededToLevelUp;

        level.text = playerStats.Level.ToString();

        //--------------------------------------

        maxLife.text = s_maxlife + playerStats.MaxLife.ToString();
        maxStamina.text = s_maxstamina + playerStats.MaxStamina.ToString();
        maxXp.text = s_maxxp + playerStats.XpNeededToLevelUp.ToString();
        damage.text = s_maxdamage + playerStats.Damage.ToString();
    }
}
