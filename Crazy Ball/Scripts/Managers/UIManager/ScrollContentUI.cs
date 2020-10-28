using UnityEngine;

public class ScrollContentUI : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform stringRect;

    public void ScrollTable(int countStr)
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, stringRect.sizeDelta.y * countStr);
    }
}
