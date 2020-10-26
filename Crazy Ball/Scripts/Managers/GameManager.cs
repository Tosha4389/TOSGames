using UnityEngine;


public class GameManager : MonoBehaviour
{
    static internal GameManager S;

    [SerializeField] UIManager uiManager;
    IncreaseScoreScript increaseScore;
    SpawnEnemyScript spawnEnemy;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);            
        }

        increaseScore = GetComponent<IncreaseScoreScript>();
        spawnEnemy = GetComponent<SpawnEnemyScript>();
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void IncreaseScore()
    {
        increaseScore.IncreaseScore();
        uiManager.ViewScoreUI(increaseScore.Score);
    }

    void SpawnEnemy()
    {
        StartCoroutine(spawnEnemy.SpawnEnemy());
    }

    public void GameOver()
    {
        uiManager.OnOffMenu();
        uiManager.SaveScoreMenu();
    }
}
