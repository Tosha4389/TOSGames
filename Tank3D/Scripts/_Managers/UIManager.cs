using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable] public class FloatEvent : UnityEvent<float> { }

public class UIManager : MonoBehaviour
{
    static internal UIManager S;
    internal FloatEvent eventScaleStripHp;
    internal FloatEvent eventScaleStripShield;

    [Header("ИГРОВЫЕ")]
    [SerializeField] GameObject gameOver;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] GameObject gameTextGO;
    [SerializeField] GameObject hpStrip;
    [SerializeField] internal GameObject shieldStrip;
    [SerializeField] TextMeshProUGUI money;

    [Header("АНДРОИД")]
    [SerializeField] internal bool androidEditor = false;
    [SerializeField] internal GameObject androidUI;

    internal bool onMenu = false;
    internal bool restart = false;
    internal bool androidRuntime = false;
    internal bool winRuntime = false;
    internal CursorMode cursorMode = CursorMode.Auto;
    internal Vector2 hotSpot = Vector2.zero;

    TextMeshProUGUI gameText;


    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
            Destroy(gameObject);
        }

        OnMouseEnter();
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

        if(eventScaleStripHp == null)
            eventScaleStripHp = new FloatEvent();
        if(eventScaleStripShield == null)
            eventScaleStripShield = new FloatEvent();
    }

    void Start()
    {
        eventScaleStripHp.AddListener(HpStripScale);
        eventScaleStripShield.AddListener(ShieldStripScale);
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
