using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI : MonoBehaviour
{
    [Header("Вручную")]
    public int amountEnemy = 5;    
    public GameObject enemyTankPrefab;
    public float delayTime = 1f;


    [Header("Автоматически")]
    public BorderCheck borderCheck;
    public List<GameObject> listEnemy;
    public Transform parent;    

    private float delay;
    private float positionCameraY;

    private void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();
        parent = GetComponent<Transform>();
    }

    void Start()
    {        
        delay = delayTime;
    }
        
    void FixedUpdate()
    {
        positionCameraY = Camera.main.transform.position.y;
        transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + 15f, transform.position.z);
        if(listEnemy.Count < amountEnemy && Time.time > delay) {
            Spawn();
            delay += delayTime;
        }

        
    }

    private void Spawn()
    {        
        Instantiate(enemyTankPrefab, transform.position, transform.rotation);
        listEnemy.Add(enemyTankPrefab);
    }

}
