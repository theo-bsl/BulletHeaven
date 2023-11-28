using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighLightTextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI text;
    private Color black;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        black = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = black;
    }
}
