using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    [Header("Вручную")]
    public int amountEnemy = 5;    
    public GameObject enemyTankPrefab;



    [Header("Автоматически")]
    public BorderCheck borderCheck;
    public List<GameObject> listEnemy;
    //public Transform parent;
    private float delayTime = 1f;
    private float delay;


    private void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();
        //parent = GetComponent<Transform>();

    }

    void Start()
    {
        delay = delayTime;
    }
        
    void FixedUpdate()
    {
        
        if(listEnemy.Count < amountEnemy && Time.time > delay) {
            Spawn();
            delay += delayTime;
        }
    }

    private void Spawn()
    {
        Instantiate(enemyTankPrefab, transform.position, transform.rotation /*parent*/);
        listEnemy.Add(enemyTankPrefab);
    }

}
