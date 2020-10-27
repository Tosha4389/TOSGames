using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContentUI : MonoBehaviour
{
    [SerializeField] RectTransform content;
    [SerializeField] RectTransform stringRect;

    public void ScrollTable(int countStr)
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, stringRect.sizeDelta.y * countStr);
    }
}
