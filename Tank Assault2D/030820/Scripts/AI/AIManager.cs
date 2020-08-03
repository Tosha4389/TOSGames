using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    static public AIManager S;

    [Header("Вручную")]
    public int amountEnemy = 5;    
    public GameObject enemyTankPrefab;
    public float delayTime = 1f;


    [Header("Автоматически")]
    public BorderCheck borderCheck;
    public List<GameObject> listEnemy;
    public Transform parent;

    private GameObject enemyClone;
    private float delay;
    private float positionCameraY;
    private int spawnEnemy;
    private Vector3 spawnPosition;
    private float spawnPosX, spawnPosY;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("AIManager.Awake() - создан второй AIManager");
            //Destroy(gameObject);
        }

        listEnemy = new List<GameObject>();
        borderCheck = GetComponent<BorderCheck>();
        parent = GetComponent<Transform>();
    }

    void Start()
    {        
        delay = delayTime;
    }
        
    void Update()
    {
        positionCameraY = Camera.main.transform.position.y;
        transform.position = new Vector3(transform.position.x, positionCameraY + 15f, transform.position.z);
        Spawn();

    }

    private void Spawn()
    {

        spawnPosition = new Vector3(spawnPosX, spawnPosY, transform.position.z);

        if(listEnemy.Count < amountEnemy && Time.timeSinceLevelLoad > delay) {
            enemyClone = Instantiate(enemyTankPrefab, transform.position, transform.rotation);
            listEnemy.Add(enemyClone);
            delay += delayTime;
            spawnEnemy++;
        }           
    }

}
