using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//20

public class LoadParametersTank : MonoBehaviour
{
    static public LoadParametersTank S;

    [Header("Автоматически")]
    public int playerDamageLoad;
    public float fireRateLoad;
    public float scatterLoad;
    public float accuracyLoad;
    public float speedMoveLoad;
    public float speedRotateLoad;
    public float speedRotateTurretLoad;
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
