using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 5

public class ShellEnemy : Shell
{
    Tank tank;

    public override void Awake()
    {
        base.Awake();
        tank = Tank.S;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) {
            tank.HitPlayerTank(enemyDamage);
            Destroy(gameObject);          
        }
    }
}
