using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    //[Header("Вручную")]


    UIManager uiManager;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }
                
    }

    private void Start()
    {
        uiManager = UIManager.S;
    }

    public void GameOver()
    {
        uiManager.GameResult("Game Over!");
    }

    public void GameWin()
    {
        uiManager.GameResult("YOU WIN!!!");
    }

}
