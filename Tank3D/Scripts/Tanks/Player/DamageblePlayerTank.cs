using UnityEngine;
using UnityEngine.Events;

public class DamageblePlayerTank : MonoBehaviour, IDamagable, IIncreasable
{    
    [SerializeField] internal int hp;
    internal int maxHp;
    UIManager uiManager;


    void Awake()
    {
        maxHp = hp;
        uiManager = UIManager.S;
    }

    public void DestroyGO()
    {
        if(hp >= 0) {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DecreaseValue(int damage)
    {
        hp -= damage;
        uiManager.eventScaleStripHp?.Invoke((float)hp/maxHp);
    }

    public void IncreasableValue(int value)
    {
        hp += value;
        uiManager.eventScaleStripHp?.Invoke((float)hp /maxHp);
    }
}
