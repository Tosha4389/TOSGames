using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAI : MonoBehaviour
{
    [Header("Вручную")]    
    public int enemyHP = 100;
    public int playerDamage = 50;
    public HPBarEnemy hpBarEnemy;

    //[Header("Автоматически")]


    private GameObject lastTrigger = null;


    private void Awake()
    {
        
    }

    private void Start()
    {

    }

    void FixedUpdate()
    {
        if(enemyHP <= 0) {
            EnemyDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
                return;
            }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellPlayer")) {
            enemyHP -= playerDamage;
            hpBarEnemy.ScaleBar();
            Destroy(gameObject);            
        }
    }

    public void EnemyDestroy()
    {                  
        Destroy(gameObject);        
    }


}
