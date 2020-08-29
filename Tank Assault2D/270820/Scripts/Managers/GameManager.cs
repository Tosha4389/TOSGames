﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//50

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("Вручную")]
    public float lenghtScene = 100f;
    public Text playerHPText;
    public GameObject gameOver;
    public GameObject cursor;

    [Header("Автоматически")]
    public bool onMenu = false;
    public bool restart = false;

    Tank tank;
    Vector3 mousePos;


    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }
        Cursor.visible = false;
        tank = Tank.S;
        
    }

    private void Start()
    {
        
    }

    void Update()
    {        
        cursor.transform.position = tank.mousePosition;
        OnMenu();
    }

    public void OnMenu()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) == true && onMenu == false) {            
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            onMenu = true;
            Cursor.visible = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) == true && onMenu == true) {
            Time.timeScale = 1f;
            gameOver.SetActive(false);
            onMenu = false;
            Cursor.visible = false;                
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
