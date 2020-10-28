using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OutputScore : MonoBehaviour
{
    [SerializeField] Transform table;
    [SerializeField] GameObject stringScore;

    LoadScoreScript loadScore;    

    private void Start()
    {
        loadScore = gameObject.GetComponent<LoadScoreScript>();      
    }

    public int OutputScoreUI()
    {
        List<PlayerScore> tempList = loadScore.LoadScore();

        for(int i = 0; i < tempList.Count; i++) {
            GameObject str = Instantiate(stringScore);
            str.transform.SetParent(table);
            str.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            str.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = tempList[i].name;
            str.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = tempList[i].score.ToString();
        }

        return tempList.Count;

    }

}
