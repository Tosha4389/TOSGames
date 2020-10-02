using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text counter;

    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if(timeNow > lastInterval + updateInterval) {
            fps = (int)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

        counter.text = fps.ToString();

    }
}

