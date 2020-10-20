using UnityEngine;

public class MotorPlayerTank : MonoBehaviour
{
    InputManager inputManager;
    MovementPlayerTank movementPlayerTank;
    TurretRotatingPlayerTank turretRotatingPlayer;
    ShootingPlayerTank shootingPlayer;
    DamageblePlayerTank damageblePlayer;
    DamagableShield damagableShield;

    private void Awake()
    {
        movementPlayerTank = GetComponent<MovementPlayerTank>();
        turretRotatingPlayer = GetComponent<TurretRotatingPlayerTank>();
        shootingPlayer = GetComponent<ShootingPlayerTank>();
        damageblePlayer = GetComponent<DamageblePlayerTank>();
        damagableShield = GetComponent<DamagableShield>();
    }

    void Start()
    {        
        inputManager = InputManager.S;
        inputManager.eventMouseClick.AddListener(EventClick);
        IncreaseValue(damageblePlayer, 0);
        IncreaseValue(damagableShield, 0);
    }

    void FixedUpdate()
    {
        MovePlayerTank(movementPlayerTank, inputManager.moveX, inputManager.moveY);
        TurretRotatePlayerTank(turretRotatingPlayer, inputManager.mouseInput);
        UpdateStripUI();
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

    void DiscreaseValue(IDamagable damagable, int value)
    {
        damagable.DecreaseValue(value);
    }

    void IncreaseValue(IIncreasable increasable, int value)
    {
        increasable.IncreasableValue(value);
    }

    void UpdateStripUI()
    {
        if(damagableShield.shield > 0 && damageblePlayer.hp < damageblePlayer.maxHp) {
            int difference = damageblePlayer.maxHp - damageblePlayer.hp;
            if(difference < damagableShield.shield) {
                DiscreaseValue(damagableShield, difference);
                IncreaseValue(damageblePlayer, difference);
            } else {
                DiscreaseValue(damagableShield, damagableShield.shield);
                IncreaseValue(damageblePlayer, damagableShield.shield);
            }
        }
    }
}
