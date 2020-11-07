using UnityEngine;

public class IncreaseScore : MonoBehaviour
{
    int score;

    public int Score()
    {
        score++;
        return score;
    }
}
