using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public void SavePlayerPrefs(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);        
    }

    public void SavePlayerPrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public int LoadPlayerPrefs(string key)
    {
        return PlayerPrefs.GetInt(key, 0);
    }
}
