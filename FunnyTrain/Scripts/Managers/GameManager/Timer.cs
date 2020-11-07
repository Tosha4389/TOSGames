using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] int durationGame = 300;

    public int CountTime()
    {
        int time = durationGame - (int)Time.timeSinceLevelLoad;
        return time;
    }
}
