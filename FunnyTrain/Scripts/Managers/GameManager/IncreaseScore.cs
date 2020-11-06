using System.Collections;
using System.Collections.Generic;
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
