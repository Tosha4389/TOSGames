using System.Collections.Generic;
using UnityEngine;

public class OutputScore : MonoBehaviour
{
    [SerializeField] GameObject stringScore;

    LoadScoreScript loadScore;
    List<PlayerScore> playerScores;

    private void Start()
    {
        loadScore = gameObject.GetComponent<LoadScoreScript>();
        playerScores = loadScore.LoadScore();
    }

    public void OutputScoreUI()
    {

    }

}
