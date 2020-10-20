using UnityEngine.Events;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    static internal InputManager S;
    [HideInInspector] internal float moveX;
    [HideInInspector] internal float moveY;
    [HideInInspector] internal float mouseInput;
    [SerializeField]  internal FixedJoystick rigthJoy;
    [SerializeField] FixedJoystick leftJoy;

    internal UnityEvent eventMouseClick;

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
    
    void Update()
    {
        MouseInput();
        KeyInput();
    }

    void MouseInput()
    {
        if(uiManager.androidRuntime && !uiManager.winRuntime) {
            mouseInput = rigthJoy.Horizontal * Time.deltaTime;
            moveX = leftJoy.Horizontal * Time.deltaTime;
            moveY = leftJoy.Vertical * Time.deltaTime;
        } else {
            mouseInput = Input.GetAxis("Mouse X") * Time.deltaTime;
            moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
            moveY = Input.GetAxis("Vertical") * Time.deltaTime;
        }

        if(Input.GetMouseButton(0) && !uiManager.androidRuntime)
            eventMouseClick.Invoke();
    }

    void KeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            uiManager.OnMenu();
    }
}
