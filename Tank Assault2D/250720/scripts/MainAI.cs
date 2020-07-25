using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAI : MonoBehaviour
{
 


    void FixedUpdate()
    {
        EnemyDestroy();
    }

    private void EnemyDestroy()
    {
        if(GameManager.S.enemyHP <= 0) {            
            Destroy(gameObject);
        }
    }

}
