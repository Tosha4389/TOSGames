using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//30

public class HPBarEnemy : HPBar
{
    public MainAI enemyMain;    

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        TrackingBar();
        RotationBar();
    }

    public override void ScaleBar()
    {
        if(enemyMain.enemyHP < 100 && enemyMain.enemyHP >= 0) {
            greenBar.SetActive(true);
            redBar.SetActive(true);
        }
        scaleBar = greenBar.transform.localScale;
        greenBar.transform.localScale = new Vector3(enemyMain.enemyHP / 100f, scaleBar.y, scaleBar.z);
        greenBar.transform.localPosition = new Vector3((1f - enemyMain.enemyHP / 100f) / -2f, 0f, 0f);

    }

    public override void TrackingBar()
    {
        transform.position = enemyMain.transform.position + Vector3.up * 1.5f;
        
    }

    public override void RotationBar()
    {
        base.RotationBar();
    }

}