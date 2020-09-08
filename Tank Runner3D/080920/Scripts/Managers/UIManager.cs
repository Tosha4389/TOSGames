using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//50

public class UIManager : MonoBehaviour
{
    static public UIManager S;

    [Header("Вручную")]
    public float lenghtScene = 100f;
    public GameObject gameOver;
    public Texture2D cursorTexture;

    [Header("Автоматически")]
    public bool onMenu = false;
    public bool restart = false;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Tank tank;

    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
            Destroy(gameObject);
        }
        //Cursor.visible = false;
        tank = Tank.S;
        OnMouseEnter();
    }

    void Update()
    {   
        OnMenu();
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
        
        if(Input.GetKeyDown(KeyCode.Escape) == true && onMenu == false) {            
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            onMenu = true;
            //Cursor.visible = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) == true && onMenu == true) {
            Time.timeScale = 1f;
            gameOver.SetActive(false);
            onMenu = false;
            //Cursor.visible = false;                
        }

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");      
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

}
