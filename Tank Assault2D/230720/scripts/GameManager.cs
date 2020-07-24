using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 7

public class GameManager : MonoBehaviour
{
    static public GameManager S;

    [Header("Вручную")]
    public float lenghtScene = 100f;

    private void Awake()
    {        
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }

    }

    void Start()
    {
        
    }
        
    void Update()
    {
        
    }
}
