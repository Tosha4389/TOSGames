using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFireAI : FireAI
{
    [Header("Вручную")]
    public float reloadRate = 10f;


    private void FixedUpdate()
    {
        Fire(fireOn);
    }

    public override void Fire(bool on)
    {        
        if(Time.time > time + fireRate && on) {
            StartCoroutine(FireDelay());
            time = Time.time;
        }
    }

    IEnumerator FireDelay()
    { 
        float chanseHit = Random.value;
        for(int i = 0; i < 3; i++) {
            shellEnemy = Instantiate(shellEnemyPrefab);
            shellEnemy.transform.rotation = mainAI.turret.transform.rotation;            

            if(chanseHit >= accuracy / 100f) {
                shellEnemy.transform.Rotate(0f, 0f, Random.Range(-scatter, scatter));
                //Debug.Log(chanseHit);
            } else shellEnemy.transform.rotation = mainAI.turret.rotation;

            Vector3 turretPos = mainAI.turret.transform.localPosition + Vector3.down * 2f + Vector3.left * 0.3f + Vector3.right * 0.3f * i;
            Vector3 shellPos = mainAI.turret.transform.TransformPoint(turretPos);
            shellEnemy.transform.position = shellPos;

            yield return new WaitForSeconds(0.2f);

        }
        StopCoroutine(FireDelay());
    }

}
