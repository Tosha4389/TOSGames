using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


//50

public class UIManager : MonoBehaviour
{
    static public UIManager S;

    [Header("ИГРОВЫЕ")]
    public GameObject gameOver;
    public Texture2D cursorTexture;
    public GameObject gameTextGO;
    public GameObject hpStrip;
    public GameObject shieldStrip;
    public TextMeshProUGUI money;

    [Header("АНДРОИД")]
    public bool androidEditor = false;
    public GameObject joyR;
    public GameObject joyL;
    public GameObject androidUI;

    [HideInInspector] public Vector2 leftJoy;
    [HideInInspector] public Vector2 rightJoy;
    [HideInInspector] public bool onMenu = false;
    [HideInInspector] public bool restart = false;
    [HideInInspector] public bool androidRuntime = false;
    [HideInInspector] public bool winRuntime = false;
    [HideInInspector] public CursorMode cursorMode = CursorMode.Auto;
    [HideInInspector] public Vector2 hotSpot = Vector2.zero;
    [HideInInspector] public FixedJoystick rigthFJ;

    TextMeshProUGUI gameText;
    FixedJoystick leftFJ;

    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
            Destroy(gameObject);
        }

        OnMouseEnter();
        rigthFJ = joyR.GetComponent<FixedJoystick>();
        leftFJ = joyL.GetComponent<FixedJoystick>();
        gameText = gameTextGO.GetComponent<TextMeshProUGUI>();

        if(Application.platform == RuntimePlatform.Android /*|| Application.platform == RuntimePlatform.WindowsEditor*/) {
            androidRuntime = true;
            androidUI.SetActive(true);
            //QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            winRuntime = true;
            Application.targetFrameRate = 2000;
        }       

    }

    private void Start()
    {
        //ShieldStripScale(Tank.S.playerShield);
    }

    void Update()
    {
        InputProcessing();
    }

    void InputProcessing()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            OnMenu();

        leftJoy.x = leftFJ.Horizontal;
        leftJoy.y = leftFJ.Vertical;
        rightJoy = rigthFJ.Direction;
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnMenu()
    {
        
        if(!onMenu) {            
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            onMenu = true;
            OnMouseExit();
        } else if (onMenu) {
            Time.timeScale = 1f;
            gameOver.SetActive(false);
            OnMouseEnter();
            onMenu = false;              
        }

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");      
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }

    public void ExitInGarage()
    {
        SceneManager.LoadScene("GarageScene");
    }

    public void GameResult(string result)
    {
        gameText.text = result;
        gameTextGO.SetActive(true);
    }

    public void HpStripScale(float scale)
    {
        hpStrip.transform.localScale = new Vector3(scale, hpStrip.transform.localScale.y, hpStrip.transform.localScale.z);
    }

    public void ShieldStripScale(float scale)
    { 
        shieldStrip.transform.localScale = new Vector3(scale, shieldStrip.transform.localScale.y, shieldStrip.transform.localScale.z);
    }

    public void MoneyText(int value)
    {
        money.text = value.ToString();
    }
}
