using UnityEngine;

public class GameManager : MonoBehaviour
{
    static internal GameManager S;

    IncreaseScore score;
    UIManager uiManager;
    Timer timer;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }

        score = GetComponent<IncreaseScore>();
        timer = GetComponent<Timer>();
    }

    private void Start()
    {
        uiManager = UIManager.S;
    }

    private void FixedUpdate()
    {
        CountTime();
    }

    public void IncreaseScore()
    {
        uiManager.ViewScore(score.Score());
    }

    public void CountTime()
    {
        int min = timer.CountTime() / 60;
        int sec = timer.CountTime() - 60 * min;
        uiManager.ViewTimeGame(min, sec);

        if(timer.CountTime() <= 0)
            GameOver();
        
    }

    void GameOver()
    {
        Time.timeScale = 0;
        uiManager.GameOverMenu(score.Score());        
    }
}
