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
    private new Camera camera;
    private List<GameObject> listEnemy;
    private GameObject enemyClone;
    private float delay;
    private float positionCameraY;


    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("AIManager.Awake() - создан второй AIManager");
            //Destroy(gameObject);
        }
        listEnemy = new List<GameObject>();
        camera = Camera.main;
    }

    void Start()
    {        
        delay = delayTime;
    }
        
    void Update()
    {
        positionCameraY = camera.transform.position.y;
        transform.position = new Vector3(transform.position.x, positionCameraY + 15f, transform.position.z);
        Spawn();
    }

    private void Spawn()
    {
        if(listEnemy.Count < amountEnemy && Time.timeSinceLevelLoad > delay) {
            enemyClone = Instantiate(enemyTankPrefab, transform.position, transform.rotation);
            listEnemy.Add(enemyClone);
            delay += delayTime;            
        }           
    }

}
