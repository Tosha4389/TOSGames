using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 7

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("Вручную")]
    public float lenghtScene = 100f;
    public int playerHP = 100;
    public int enemyHP = 20;
    public Text playerHPText;

    //[Header("Автоматически")]

    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            //Destroy(gameObject);
        }
    }



    public void GameOver()
    {
        print("Геймовер!");
    }

    public void HitDamage(bool player, int dmg)
    {
        if(player) {            
            playerHP -= dmg;
            playerHPText.text = "HP: " + playerHP;
        } else {
            enemyHP -= dmg;
        }
    }



}
