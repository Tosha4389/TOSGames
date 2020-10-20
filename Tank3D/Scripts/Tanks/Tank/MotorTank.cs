using System.Collections;
using UnityEngine;

public class MotorTank : MonoBehaviour
{
    [SerializeField] float searchDistance = 50f;
    [SerializeField] float stopDistance = 20f;
    [SerializeField] Transform player;
    [SerializeField] GameObject rayPos;

    Transform tank;
    MovementTank movementTank;
    TurretRotatingTank turretRotating;
    ShootingTank shootingTank;
    float distancePlayer;

    private void Awake()
    {
        tank = GetComponent<Transform>();
        movementTank = GetComponent<MovementTank>();
        turretRotating = GetComponent<TurretRotatingTank>();
        shootingTank = GetComponent<ShootingTank>();
    }

    void Start()
    {
        StartCoroutine(GetDistanceForPlayer());
    }

    void FixedUpdate()
    {
        MoveMotor();
        RotateTurretMotor();
    }

    IEnumerator GetDistanceForPlayer()
    {
        distancePlayer = Vector3.Distance(player.position, tank.position);       
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GetDistanceForPlayer());
    }

    public void MoveMotor()
    {
        if(distancePlayer <= searchDistance && distancePlayer > stopDistance)
            movementTank.Movement(player.position);

        if(distancePlayer <= stopDistance){
            movementTank.Movement(tank.position);
        }            
    }

    public void RotateTurretMotor()
    {
        if(distancePlayer <= searchDistance) {
            turretRotating.TurretRotate(player.position);
            ShootingMotor();
        }
        else turretRotating.TurretRotate(tank.forward);
    }

    public void ShootingMotor()
    {        
        RaycastHit hit;
        Vector3 direction = player.position - tank.position;
        if(Physics.Raycast(rayPos.transform.position, direction, out hit, searchDistance)) {
            if(hit.collider.CompareTag("Player")) {                
                shootingTank.Shooting();
            }
        }
    }
    

}
