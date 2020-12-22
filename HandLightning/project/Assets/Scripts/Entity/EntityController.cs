using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum Relation
{
    friend,
    enemy
}

public class EntityController : MonoBehaviour 
{
    [Tooltip("Друг или враг")]
    [SerializeField] Relation currentRelation;
    [Tooltip("Здоровье существа")]
    [SerializeField] float health = 100f;
    [Tooltip("Очки за смерть друга")]
    [SerializeField] int friendScore = 3;
    [Tooltip("Очки за смерть врага")]
    [SerializeField] int enemyScore = 1;

    EntityMove entityMove;
    SpriteRenderer spriteRenderer;
    Transform tran;
    GameManager gameManager;

    float startSpeed;
    float minHealth = 100f;
    float coeff;

    public Relation CurrentRelation
    {
        set { currentRelation = value; }
    }

    public float Health
    {
        set { health = value; }
    }

    private void Awake()
    {
        entityMove = GetComponent<EntityMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tran = GetComponent<Transform>();
    }

    private void Start()
    {
        gameManager = GameManager.S;
    }

    void Update()
    {
        entityMove.Move();
    }

    public void InitEntity()
    {
        tran.localScale = Vector2.one;
        coeff = health / minHealth;
        startSpeed = entityMove.Speed;
        entityMove.Speed /= coeff; 

        switch(currentRelation) {
            case Relation.enemy:
                spriteRenderer.color = Color.red;
                tran.localScale *= coeff;
                break;
            case Relation.friend:
                spriteRenderer.color = Color.green;
                tran.localScale *= 1.5f;
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.parent.GetComponent<LightningScript>() != null) {
            DamageEntity(collision.transform.parent.GetComponent<LightningScript>().Damage);
        }
    }

    void DamageEntity(float damage)
    {
        if(health > 0) {
            health -= damage;            
        } else EntityDie();

        if(tran.localScale.x > 1f)
            tran.localScale -= Vector3.one * (coeff - 1f) / damage;
    }

    void EntityDie()
    {
        switch(currentRelation) {
            case Relation.enemy:
                gameManager.CountScore(enemyScore);
                break;
            case Relation.friend:
                gameManager.CountScore(-friendScore);
                break;
        }

        entityMove.Speed = startSpeed;
        gameObject.SetActive(false);
        gameManager.ActiveOtherEntity(currentRelation);
    }
}
