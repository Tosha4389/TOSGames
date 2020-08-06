using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//25

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("Вручную")]
    public float lenghtScene = 100f;
    public int playerHP = 100;
    public int enemyDamage = 10;
    public Text playerHPText;
    public GameObject gameOver;

    [Header("Автоматически")]
    public bool onMenu = false;
    public bool restart = false;
    

    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            //Destroy(gameObject);
        }        
    }

    private void Start()
    {
        
    }

    void Update()
    {        
        OnMenu();
    }

    public void OnMenu()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) == true && onMenu == false) {            
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            onMenu = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) == true && onMenu == true) {
            Time.timeScale = 1f;
            gameOver.SetActive(false);
            onMenu = false;
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

    public void HitDamage()
    {
        playerHP -= enemyDamage;
        //playerHPText.text = "HP: " + playerHP;
    }



}
