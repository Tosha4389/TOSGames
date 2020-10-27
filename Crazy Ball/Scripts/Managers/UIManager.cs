using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    static internal UIManager S;

    [SerializeField] internal Text text;
    [SerializeField] internal TextMeshProUGUI scoreText;

    IMenuUI menuUI;  
    LoadScoreScript loadScore;
    SaveScoreScript saveScore;
    OutputScore outputScore;
    ScrollContentUI scrollContent;
    bool onMenu = false;
    internal int ScoreSave { get; set; }

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
            Destroy(gameObject);
        }
        menuUI = GetComponent<IMenuUI>();
        loadScore = GetComponent<LoadScoreScript>();
        saveScore = GetComponent<SaveScoreScript>();
        outputScore = GetComponent<OutputScore>();
        scrollContent = GetComponent<ScrollContentUI>();
    }

    private void Start()
    {
        StartCoroutine(OutputScoreTable());
    }

    public void ViewScoreUI(int score)
    {
        ScoreSave = score;
        scoreText.text = "Score: " + score;
    }

    public void StartNewGame()
    {
        menuUI.LoadSceneMenu(1);
    }

    public void ViewTopScore()
    {      
        menuUI.LoadSceneMenu(2);        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        menuUI.LoadSceneMenu(1);
    }

    public void ExitMainMenu()
    {
        menuUI.LoadSceneMenu(0);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void OnOffMenu()
    {
        menuUI.OnOffMenu();
    }

    public void LoadScore()
    {
        loadScore.LoadScore();
    }

    public void SaveScoreMenu()
    {
        saveScore.SaveScoreMenu();
    }

    public IEnumerator OutputScoreTable()
    {
        yield return new WaitForSeconds(0.1f);
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            int str = outputScore.OutputScoreUI();
            scrollContent.ScrollTable(str);
        }        
    }
}
