using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] float gameDuration;
    [SerializeField] TextMeshProUGUI scoreCount;
    [SerializeField] GameObject resultMenu;

    TextMeshProUGUI scoreResult;
    int endScore;    

    private void Start()
    {
        if(timer) 
            StartCoroutine(UpdateTimer());
    }

    private void Update()
    {
        BackButtonInit();
    }

    public void ViewScore(int score)
    {
        scoreCount.text = $"Score: {score}";
        endScore = score;
    }

    IEnumerator UpdateTimer()
    {
        if(gameDuration >= 10f)
            timer.text = $"0:{gameDuration}";
        else timer.text = $"0:0{gameDuration}";


        yield return new WaitForSeconds(1f);

        if(gameDuration > 0f) {
            gameDuration -= 1f;
            StartCoroutine(UpdateTimer());
        } else {
            GameOver();
            StopCoroutine(UpdateTimer());
        }
    }

    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1f;
    }

    void GameOver()
    {
        GameMenu();
        resultMenu.transform.GetChild(0).gameObject.SetActive(true);
        scoreResult = resultMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreResult.text = $"Score: {endScore}";
    }

    public void GameMenu()
    {
        if(!resultMenu.activeInHierarchy) {
            resultMenu.SetActive(true);
            Time.timeScale = 0f;
        } else {
            resultMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
            GameMenu();
    }

    void BackButtonInit()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
            switch(SceneManager.GetActiveScene().buildIndex) {
                case 0:
                    Application.Quit();
                    break;
                case 1:
                    SceneManager.LoadScene(0);
                    break;
            }
    }

}
