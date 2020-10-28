using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class LoadScoreScript : MonoBehaviour
{    
    string path;

    public List<PlayerScore> LoadScore()
    {
        List<PlayerScore> scoreList = new List<PlayerScore>();

        if(Application.platform == RuntimePlatform.Android) {            
            path = Path.Combine(Application.persistentDataPath, "PlayerName.json");
        }


        if(Application.platform == RuntimePlatform.WindowsEditor)
            path = Application.dataPath + @"/PlayerName.json";

        if(!File.Exists(path)) {
            File.AppendAllText(path, "[]");
            return scoreList;
        }

        try {
            string json = File.ReadAllText(path);
            scoreList = JsonConvert.DeserializeObject<List<PlayerScore>>(json);
            //Debug.Log("Лист загружен");
            ListComparer comparer = new ListComparer();
            scoreList.Sort(comparer);
            return scoreList;
        }
        catch(Exception) {
            //Debug.Log("Лист не загружен");
            return scoreList;
        }
    }
}
