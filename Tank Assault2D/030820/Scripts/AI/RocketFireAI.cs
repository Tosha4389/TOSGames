using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFireAI : FireAI
{
    [Header("Вручную")]
    public float reloadRate = 10f;

    float timeReload;
    Vector3 turretPos;

    void Awake()
    {
        turretPos = mainAI.turret.transform.position;
    }

    private void Start()
    {
        timeReload = 0f; ;
    }

    private void FixedUpdate()
    {
        Fire(true);
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
        for(int i = 0; i < 5; i++) {
            shellEnemy = Instantiate(shellEnemyPrefab);
            shellEnemy.transform.rotation = mainAI.turret.transform.rotation;
            shellEnemy.transform.position = new Vector3(turretPos.x - 0.4f + 0.2f * i, turretPos.y, turretPos.z);
            yield return new WaitForSeconds(0.2f);

        }
        StopCoroutine(Wait());
    }

}
