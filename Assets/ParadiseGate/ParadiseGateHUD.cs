using UnityEngine;
using UnityEngine.UI;

public class ParadiseGateHUD : MonoBehaviour
{
    private ParadiseGateStats paradiseGateStats;

    public Image paradiseGateLife;

    private void Awake()
    {
        paradiseGateStats = ParadiseGateStats.Instance;
    }

    private void Update()
    {
        paradiseGateLife.fillAmount = paradiseGateStats.Life / paradiseGateStats.MaxLife;
    }
}
