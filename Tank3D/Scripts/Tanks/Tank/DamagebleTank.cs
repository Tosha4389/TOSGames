using UnityEngine;

public class DamagebleTank : MonoBehaviour, IDamagable
{
    [SerializeField] int hp = 100;
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.S;
    }

    public void DecreaseValue(int damage)
    {
        hp -= damage;

        if(hp <= 0) {
            DestroyGO();
        }
    }

    public void DestroyGO()
    {
        //agent.enabled = false;
        Destroy(gameObject);
        gameManager.CreateBonus(transform.position);
        //GameObject explo = Instantiate(firePrefab, gameObject.transform.position, Quaternion.identity);
        //Destroy(explo, 1f);
    }
}
