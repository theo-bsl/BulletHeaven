using TMPro;
using UnityEngine;

public class MenuGameOver : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public TextMeshProUGUI kills;

    private void Start()
    {
        PlayerStats player = PlayerStats.Instance;

        score.text = score.text.Substring(0, score.text.Length - 1) + player.Score.ToString();
        time.text = time.text.Substring(0, time.text.Length - 1) + ((int)(Time.time - player.StartTime)).ToString();
        kills.text = kills.text.Substring(0, kills.text.Length - 1) + player.NbKill.ToString();
    }
}
