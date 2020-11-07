using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class UIManager : MonoBehaviour
{
    static internal UIManager S;

    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TextMeshProUGUI scoreGameOver;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
            Destroy(gameObject);
        }
    }

    public void ViewScore(int score)
    {
        scoreUI.text = "Score: " + score;
    }

    public void ViewTimeGame(int min, int sec)
    {
        if(sec < 10)
            timer.text = min + ":0" + sec;
        else timer.text = min + ":" + sec;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOverMenu(int score)
    {
        gameOverMenu.SetActive(true);
        scoreGameOver.text = score.ToString();
    }
}
