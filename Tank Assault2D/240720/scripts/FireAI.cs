using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 20

public class FireAI : MonoBehaviour
{
    [Header("Вручную")]
    public float fireRate;
    public GameObject shellEnemyPrefab;



    [Header("Автоматически")]
    public GameObject shellEnemy;
    public Transform turret;
    public MoveAI moveAI;

    //private Transform parent;
    private IEnumerator coroutine;

    private void Awake()
    {
        moveAI = GetComponent<MoveAI>();
        turret = gameObject.transform.GetChild(0);
        //parent = GetComponent<Transform>();
    }

    private void Start()
    {
        coroutine = Fire(fireRate);
        StartCoroutine(coroutine);
    }


    void FixedUpdate()
    {
        turret.transform.up = -moveAI.distanceTarget;        
    }

    private IEnumerator Fire(float wait)
    {
        while(true) {
            shellEnemy = Instantiate(shellEnemyPrefab /*parent*/);
            shellEnemy.transform.position = turret.transform.position;
            shellEnemy.transform.rotation = turret.transform.rotation;
            yield return new WaitForSeconds(wait);
        }
    }

}
