using UnityEngine;

public class IncreaseScoreScript : MonoBehaviour
{
    internal int Score { get; set; }
    public void IncreaseScore()
    {
        Score += 1;        
    }
}
