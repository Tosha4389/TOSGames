using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerScore
{
    public string name;
    public int score;

    public PlayerScore(string n, int s)
    {
        name = n;
        score = s;
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }
}
