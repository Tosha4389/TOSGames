using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//50

public class AIManager : MonoBehaviour
{
    static public AIManager S;

    [Header("Вручную")]
    public int amountEnemy = 5;    
    public GameObject enemyTankPrefab;
    public float delayTime = 1f;

    [Header("Автоматически")]
    private List<GameObject> listEnemy;
    private float delay;


    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("AIManager.Awake() - создан второй AIManager");
            //Destroy(gameObject);
        }
        listEnemy = new List<GameObject>();
    }

    void Start()
    {        
        delay = delayTime;
    }
        
    void Update()
    {        
        Spawn();
    }

    private void Spawn()
    {
        if(listEnemy.Count < amountEnemy && Time.timeSinceLevelLoad > delay) {
            GameObject enemyClone = Instantiate(enemyTankPrefab, transform.position, enemyTankPrefab.transform.rotation);
            listEnemy.Add(enemyClone);
            delay += delayTime;            
        }           
    }

}
