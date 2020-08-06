using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFireAI : FireAI
{
    [Header("Вручную")]
    public float reloadRate = 10f;


    void Awake()
    {
        
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Fire(fireOn);
    }

    public override void Fire(bool on)
    {        
        if(Time.time > time + fireRate && on) {
            StartCoroutine(Wait());
            time = Time.time;
        }
    }

    IEnumerator Wait()
    {
        
        Vector3 turretPos = mainAI.turret.transform.localPosition + Vector3.down * 2f;
        Vector3 shellPos = mainAI.turret.transform.TransformPoint(turretPos);

        for(int i = 0; i < 5; i++) {
            shellEnemy = Instantiate(shellEnemyPrefab);
            shellEnemy.transform.rotation = mainAI.turret.transform.rotation;
            shellEnemy.transform.position = new Vector3(shellPos.x - 0.4f + 0.2f * i, shellPos.y, shellPos.z);
            yield return new WaitForSeconds(0.2f);

        }
        StopCoroutine(Wait());
    }

}
