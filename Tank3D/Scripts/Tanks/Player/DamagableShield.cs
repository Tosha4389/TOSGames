using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableShield : MonoBehaviour, IDamagable, IIncreasable
{
    [SerializeField] internal int shield;
    [SerializeField] internal int maxShield;
    [SerializeField] GameObject shieldGO;
    UIManager uiManager;    

    void Awake()
    {
        uiManager = UIManager.S;
    }

    public void DecreaseValue(int damage)
    {
        shield -= damage;
        uiManager.eventScaleStripShield?.Invoke((float)shield / maxShield);
        if(shield <= 0)
            DestroyGO();
    }

    public void IncreasableValue(int value)
    {
        shield += value;
        uiManager.eventScaleStripShield?.Invoke((float)shield / maxShield);
        if(shield > 0) {
            EnabledGO();
        }

    }

    public void DestroyGO()
    {
        shieldGO.SetActive(false);
    }

    void EnabledGO()
    {
        shieldGO.SetActive(true);
    }
}
