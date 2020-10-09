using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//20

public class LoadParametersTank : MonoBehaviour
{
    static public LoadParametersTank S;

    [Header("Автоматически")]
    public int playerHp;
    public int playerShield;
    public int playerDamage;
    public float fireRate;
    public float scatter;
    public float accuracy;
    public float speedMove;
    public float speedRotate;
    public float speedRotateTurret;
    public Vector3 gunPos;
    public Sprite[] tankSprites = new Sprite[3];

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {            
            Destroy(gameObject);
        }        
        
        DontDestroyOnLoad(gameObject);
    }

}
