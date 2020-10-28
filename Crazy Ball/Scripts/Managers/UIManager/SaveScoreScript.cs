using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

public class SaveScoreScript : MonoBehaviour
{
    
    [SerializeField] GameObject menu;
    [SerializeField] GameObject enterName;
    [SerializeField] Text inputName;

    UIManager uiManager;
    LoadScoreScript loadScore;
    string path;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        loadScore = gameObject.GetComponent<LoadScoreScript>();
    }

    public void SaveScoreMenu()
    {
        menu.SetActive(false);
        enterName.SetActive(true);
    }

    public void OkClick()
    {
        if(inputName.text != "") {
            SaveScore();
            enterName.SetActive(false);
            menu.SetActive(true);
            uiManager.LoadScore();
        } else Debug.Log("Введите имя");
    }

    public void NoClick()
    {        
        enterName.SetActive(false);
        menu.SetActive(true);
    }

    public void SaveScore()
    {
        if(Application.platform == RuntimePlatform.Android)
            path = Path.Combine(Application.persistentDataPath, "PlayerName.json");
        else if(Application.platform == RuntimePlatform.WindowsEditor)          
            path = Application.dataPath + @"/PlayerName.json";

        List<PlayerScore> tempList = loadScore.LoadScore();
        PlayerScore playerScore = new PlayerScore(inputName.text, uiManager.ScoreSave);
        tempList.Add(playerScore);

        string json = JsonConvert.SerializeObject(tempList, Formatting.Indented);

        try {
            File.WriteAllText(path, json);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        tempList.Clear();
    }
}
