using UnityEngine.Events;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    static public InputManager S;
    [HideInInspector] public float moveX;
    [HideInInspector] public float moveY;
    [HideInInspector] public float mouseInput;

    [HideInInspector] public UnityEvent eventMouseClick;

    UIManager uiManager;

    void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("InputManager.Awake() - создан второй InputManager");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        uiManager = UIManager.S;
        if(eventMouseClick == null)
            eventMouseClick = new UnityEvent();
    }
    
    void FixedUpdate()
    {
        MouseInput();
        KeyInput();
    }

    void MouseInput()
    {
        if(uiManager.androidRuntime && !uiManager.winRuntime) {
            mouseInput = uiManager.rigthFJ.Horizontal * Time.deltaTime;
        } else {
            mouseInput = Input.GetAxis("Mouse X") * Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && !uiManager.androidRuntime)
            eventMouseClick.Invoke();
    }

    void KeyInput()
    {
        if(uiManager.androidRuntime && !uiManager.winRuntime) {            
            moveX = uiManager.leftJoy.x * Time.deltaTime * 1.1f;
            moveY = uiManager.leftJoy.y * Time.deltaTime * 1.1f;
        } else {
            moveX = Input.GetAxis("Horizontal") * Time.deltaTime * 1.1f;
            moveY = Input.GetAxis("Vertical") * Time.deltaTime;
        }
    }
}
