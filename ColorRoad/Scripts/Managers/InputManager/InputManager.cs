using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    internal static InputManager S;
    internal bool moveOn;

    private void Awake()
    {
        if(S == null)
            S = this;
        else {
            Debug.Log( gameObject.name + ".Awake(): Ошибка. Создан второй " + gameObject.name);
            Destroy(this.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!moveOn)
            moveOn = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(moveOn)
            moveOn = false;
    }
}
