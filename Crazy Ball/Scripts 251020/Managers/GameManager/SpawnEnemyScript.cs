using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float slowDown;
    float time;

    private void Awake()
    {
        time = Time.time;
    }

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(slowDown);
        int i = Random.Range(0, enemyList.Count);
        Instantiate(enemyList[i], spawnPoint.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }
}
