using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    hp,
    shield,
    money,
    damage
}

public class Bonus : MonoBehaviour
{
    [Header("Вручную:")]
    public BonusType currentBonusType;
    public int increaseValue = 25;

    [Header("Вращение:")]
    public float speed = 200f;
    public float speedLights = 500f;

    Tank tank;
    GameManager gameManager;
    UIManager uIManager;
    Transform lightR;
    Transform lightL;
    AudioSource audio;
    MeshRenderer mesh;
    Vector3 axisRotateR;
    Vector3 axisRotateL;

    private void Awake()
    {
        try {
            lightL = gameObject.transform.GetChild(0);
        }
        catch {
            Debug.Log("У объекта " + gameObject.name + " нет дочернего элемента с индексом 0.");
        }

        try {
            lightR = gameObject.transform.GetChild(1);
        }
        catch {
            Debug.Log("У объекта " + gameObject.name + " нет дочернего элемента с индексом 1.");
        }
        audio = GetComponent<AudioSource>();
        mesh = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        tank = Tank.S;
        gameManager = GameManager.S;
        uIManager = UIManager.S;
        axisRotateR = Vector3.Normalize(new Vector3(1f, 1f, 0f) - new Vector3(-1f, -1f, 0f));
        axisRotateL = Vector3.Normalize(new Vector3(-1f, 1f, 0f) - new Vector3(1f, -1f, 0f));
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
        if(lightL != null)
            lightL.transform.RotateAround(transform.position, axisRotateR, speedLights * Time.deltaTime);
        if(lightR != null)
            lightR.transform.RotateAround(transform.position, axisRotateL, -speedLights * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {            
            switch(currentBonusType) {
                case BonusType.hp:
                    IncreaseHp(increaseValue);
                    break;

                case BonusType.shield:
                    IncreaseShield(increaseValue);
                    break;

                case BonusType.money:
                    IncreaseMoney(increaseValue);
                    break;

                case BonusType.damage:
                    IncreaseDamage(increaseValue);
                    break;
            }

            if(!audio.isPlaying)
                audio.PlayOneShot(audio.clip);        
            mesh.enabled = false;
            Destroy(gameObject, 0.5f);
        }
    }

    void IncreaseHp(int value)
    {
        int diffHP = tank.maxPlayerHP - tank.playerHP;
        if(diffHP >= value)
            tank.playerHP += value;
        else tank.playerHP += diffHP;
        tank.UpdateStatsUI();
    }

    void IncreaseShield(int value)
    {
        int diffShield = tank.maxPlayerShield - tank.playerShield;
        if(diffShield >= value)
            tank.playerShield += value;
        else tank.playerShield += diffShield;
        tank.UpdateStatsUI();
    }

    void IncreaseMoney(int value)
    {
        gameManager.PlayerMoney(value, true);
    }

    void IncreaseDamage(int value)
    {
        tank.playerDamage += value;
    }

}
