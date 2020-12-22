using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    [Tooltip("Количество активных существ")]
    [SerializeField] int maxActiveEntities = 3;
    [Tooltip("Соотношение враги/дружеские")]
    [SerializeField] float ratioEnemyFriend = 1f;
    [Tooltip("Максимальное здоровье врага (> 100)")]
    [SerializeField] float maxEnemyHeath = 200f;
    [SerializeField] Transform entities;
    [SerializeField] Transform spawn;

    EntityController[] entityControllers;
    Transform[] spawnPoints;
    int enemyCount;
    int friendCount;

    private void Awake()
    {
        entityControllers = entities.GetComponentsInChildren<EntityController>();
        spawnPoints = spawn.GetComponentsInChildren<Transform>();

        if(maxActiveEntities > entityControllers.Length)
            maxActiveEntities = entityControllers.Length;

        for(int i = 0; i < entityControllers.Length; i++)
                entityControllers[i].gameObject.SetActive(false);


        friendCount = Mathf.RoundToInt(maxActiveEntities / (ratioEnemyFriend + 1f));
        enemyCount = maxActiveEntities - friendCount;        
    }

    public void ActiveEntities()
    {
        for(int i = 0; i < entityControllers.Length; i++) {
            if(!entityControllers[i].gameObject.activeInHierarchy && enemyCount != 0) {
                ActiveEntity(entityControllers[i], Relation.enemy);
                enemyCount--;
            }
        }

        for(int i = 0; i < entityControllers.Length; i++) {
            if(!entityControllers[i].gameObject.activeInHierarchy && friendCount != 0) {
                ActiveEntity(entityControllers[i], Relation.friend);
                friendCount--;
            }
        }
    }
    public void ActiveEntity(Relation relation)
    {
        for(int i = 0; i < entityControllers.Length; i++) {
            if(!entityControllers[i].gameObject.activeInHierarchy) {
                ActiveEntity(entityControllers[i], relation);
                return;
            }
        }
    }

    public void ActiveEntity(EntityController entity ,Relation relation)
    {
        entity.gameObject.SetActive(true);
        entity.gameObject.GetComponent<Rigidbody2D>().MovePosition(spawnPoints[Random.Range(1, 4)].transform.position);
        entity.CurrentRelation = relation;
        entity.Health = 100f + Random.Range(0f, maxEnemyHeath - 100f);
        entity.InitEntity();
    }
}
