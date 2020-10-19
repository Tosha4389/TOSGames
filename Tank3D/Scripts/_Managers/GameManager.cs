using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("Вручную")]
    public List<GameObject> bonus;

    int playerMoney = 0;
    UIManager uiManager;
    //public List<GameObject> walls;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }
        //walls = new List<GameObject>();
    }

    private void Start()
    {
        uiManager = UIManager.S;
        //StartCoroutine(GetWalls());
    }

    public void GameOver()
    {
        uiManager.GameResult("Game Over!");
    }

    public void GameWin()
    {
        uiManager.GameResult("YOU WIN!!!");
    }

    public void PlayerMoney(int value, bool increase)
    {
        if(increase)
            playerMoney += value;
        else playerMoney -= value;
        uiManager.MoneyText(playerMoney);
    }

    public void CreateBonus(Vector3 position)
    {
        float chanse = Random.value;
        if(chanse >= 0 && chanse < 0.2)
            Instantiate(bonus[0], position, Quaternion.identity);
        if(chanse >= 0.2 && chanse < 0.4)
            Instantiate(bonus[1], position, Quaternion.identity);
        if(chanse >= 0.4 && chanse < 0.6)
            Instantiate(bonus[2], position, Quaternion.identity);
        if(chanse >= 0.6 && chanse < 0.8)
            Instantiate(bonus[3], position, Quaternion.identity);
    }

    //IEnumerator GetWalls()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Debug.Log(walls.Count);
    //}
}
