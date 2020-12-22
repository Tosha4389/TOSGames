using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    UIManager uiManager;
    EntityFactory entityFactory;
    int score;

    void Awake()
    {
        if(S == null)
            S = this;
        else {
            Debug.LogError("GameManager.Awake() создан второй GameManager.");
            Destroy(this);
        }

        uiManager = GetComponent<UIManager>();
        entityFactory = GetComponent<EntityFactory>();
    }

    private void Start()
    {
        if(entityFactory)
            entityFactory.ActiveEntities();
    }

    public void CountScore(int value)
    {
        int diff = score + value;
        if(score >= 0 && diff >= 0) {
            score += value;
        } else if(diff < 0)
            score = 0;

        uiManager.ViewScore(score);
    }

    public void ActiveOtherEntity(Relation relation) => entityFactory.ActiveEntity(relation);

}
