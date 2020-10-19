using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageblePlayerTank : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;

    public void DestroyGO()
    {
        if(hp >= 0) {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DecreaseHp(int damage)
    {
        hp -= damage;
    }

    public void EnlargeHp(int value)
    {
        hp += value;
    }
}
