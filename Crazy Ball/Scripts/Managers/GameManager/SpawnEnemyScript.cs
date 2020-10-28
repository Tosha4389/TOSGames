using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform target;
    [SerializeField] float slowDown;

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(slowDown);
        int random = Random.Range(0, enemyList.Count);
        Instantiate(enemyList[random], spawnPoint.position, Quaternion.identity);
        StartCoroutine(SpawnEnemy());
    }


}
