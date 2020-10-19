using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorPlayerTank : MonoBehaviour
{
    InputManager inputManager;
    MovementPlayerTank movementPlayerTank;
    TurretRotatingPlayerTank turretRotatingPlayer;
    ShootingPlayerTank shootingPlayer;

    private void Awake()
    {
        movementPlayerTank = GetComponent<MovementPlayerTank>();
        turretRotatingPlayer = GetComponent<TurretRotatingPlayerTank>();
        shootingPlayer = GetComponent<ShootingPlayerTank>();
    }

    void Start()
    {        
        inputManager = InputManager.S;
        inputManager.eventMouseClick.AddListener(EventClick);
    }

    void FixedUpdate()
    {
        MovePlayerTank(movementPlayerTank, inputManager.moveX, inputManager.moveY);
        TurretRotatePlayerTank(turretRotatingPlayer, inputManager.mouseInput);
    }

    void MovePlayerTank(IMovement movable, float moveX, float moveY)
    {
        movable.Movement(moveX, moveY);
    }

    void TurretRotatePlayerTank(ITurretRotating turretRotate, float mouseInput)
    {
        turretRotate.TurretRotate(mouseInput);
    }

    void ShootingPlayerTank(IShooting shooting)
    {
        shooting.Shooting();
    }

    void EventClick()
    {
        ShootingPlayerTank(shootingPlayer);
    }
}
