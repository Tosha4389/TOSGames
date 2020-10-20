using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotatingPlayerTank : MonoBehaviour, ITurretRotating
{
    public float speedRotateTurret;
    Transform turret;
    InputManager inputManager;

    public void TurretRotate(float mouseInput)
    {
        turret.eulerAngles = new Vector3(0f, turret.eulerAngles.y + mouseInput * speedRotateTurret * 4f, 0f);
    }
    
    void Awake()
    {
        turret = gameObject.transform.GetChild(0);
        inputManager = InputManager.S;
    }
}
