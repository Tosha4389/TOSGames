using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//250

public class MainAI : MonoBehaviour
{
    [Header("Вручную")]    
    public int enemyHP = 100;
    public float searchDistanseCoeff = 10f;
    public float speedRotTurret = 1f;
    public float fireRate;
    public float accuracy = 80f;
    public float scatter = 20f;
    public GameObject shellEnemyPrefab;
    public HPBarEnemy hpBarEnemy;
    public GameObject explosionPrefab;
    public GameObject firePrefab;
    public GameObject rayPos;

    [Header("Автоматически")]
    public int playerDamage;
    public float searchDistance;
    public float time;
    public GameObject shellEnemy;
    public BorderCheck borderCheck;
    public new Rigidbody2D rigidbody;
    public Transform turret;
    public NavMeshAgent agent;    
    public Tank tank;
    public Vector3 tankPosition;
    public Vector3 distanceTank;
    public bool moveOn = false;
    public bool destroy;
    public bool playerFound = false;
    public bool enemyManeuver = false;
    public bool enemyEscape = false;

    GameObject lastTrigger = null;
    float rotTime;
    float rayTime;
    float waveFrequency = 25f;
    float waveWidth = 2f;    
    float escapeX, escapeY;
    Vector3 rotTurret;


    public virtual void Awake()
    {
        borderCheck = GetComponent<BorderCheck>();        
        turret = gameObject.transform.GetChild(0);
        rigidbody = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        tank = Tank.S;
    }

    public virtual void Start()
    {
        rayTime = rotTime = time = Time.time;        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public virtual void FixedUpdate()
    {
        if(rayTime < 1f) {
            playerDamage = tank.playerDamage;
        }
                
        //float xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        //float yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        //transform.position = new Vector3(xLimit, yLimit, transform.position.z);

        DistanceTank();

        if(Time.time > rayTime + 1f) {
            FindPlayer();
            EnemyAttack();
            rayTime = Time.time;
        }

        //FindWay();
        EnemyManeuver();
        RotateTurret();        
        EnemyDestroy();
        EnemyMoving();
        EnemyEscape();     

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
                return;
            }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellPlayer")) {
            enemyHP -= playerDamage;
            hpBarEnemy.ScaleBar();
            Destroy(gameObject);
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
    }


    private Vector3 DistanceTank()
    {
        if(tank) {
            tankPosition = tank.transform.position;
        } else {
            Debug.Log("Объект танк не обнаружен!");
            return Vector3.zero;
        }

        distanceTank = tankPosition - transform.position;
        searchDistance = borderCheck.screenHeight * searchDistanseCoeff / 5f;
        return distanceTank;
    }


    private void FindPlayer()
    {        
        if(distanceTank.magnitude <= searchDistance) {
            playerFound = true;
        }

        // Поиск союзника, передача ему playerFound = true

        Vector2 circlePos = transform.localPosition;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Collider2D[] hitCollider = Physics2D.OverlapCircleAll(circlePos, searchDistance, layerMask);
        foreach(Collider2D alies in hitCollider) {
            if(playerFound && alies.CompareTag("Enemy")) {
                alies.GetComponent<MainAI>().playerFound = true;
            }
        }
        hitCollider = null;
    }


    private void FindWay()
    {
        Vector3 enemyPos = transform.localPosition - transform.up * 2f;
        Vector2 rayPos = new Vector2(enemyPos.x, enemyPos.y);
        RaycastHit2D way = Physics2D.Raycast(rayPos, -transform.up, 2f);
        if(way) {
            
        }

    }


    public virtual void EnemyMoving()
    {
        if(distanceTank.magnitude <= searchDistance || playerFound) {
            if(enemyEscape || enemyManeuver) {
                return;
            }

            agent.destination = tank.transform.position;
            Vector3 nextPoint = new Vector3(agent.steeringTarget.x, agent.steeringTarget.y, transform.position.z);
            Vector3 direction = nextPoint - transform.position;
            float step = Time.fixedDeltaTime * 2f;
            transform.up = Vector3.Lerp(transform.up, -direction, step);

            //rigidbody.AddRelativeForce(Vector3.down * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);             
        }
    }


    private void EnemyEscape()
    {
        if(enemyHP <= 50) {
            enemyEscape = true;

            if(transform.position.y == escapeY || escapeY == 0) {
                escapeX = Random.Range(-borderCheck.screenWidth, borderCheck.screenWidth);
                escapeY = borderCheck.camera.transform.position.y + borderCheck.camera.orthographicSize * 2f;
                agent.destination = new Vector3(escapeX, escapeY, transform.position.z);
            }
                Vector3 nextTarget = new Vector3(agent.steeringTarget.x, agent.steeringTarget.y, transform.position.z);
                Vector3 direction = transform.position - nextTarget;
                float step = Time.fixedDeltaTime * 1.5f;
                transform.up = Vector3.Lerp(transform.up, direction, step);


            //rigidbody.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);
        } else enemyEscape = false;
    }


    private void EnemyManeuver()
    {
        if(distanceTank.sqrMagnitude <= 40f) {
            enemyManeuver = true;
            agent.velocity = Vector3.zero;
            //rigidbody.AddRelativeForce(Vector3.zero * Time.fixedDeltaTime * speedMove * 2f, mode: ForceMode2D.Impulse);
        } else enemyManeuver = false;
    }


    private void RotateTurret()
    {  

        if(distanceTank.magnitude >= searchDistance) {
            float age = Time.time - rotTime;
            float theta = Mathf.PI * 2 * age * speedRotTurret / waveFrequency;
            float sin = Mathf.Sin(theta);
            rotTurret.z = borderCheck.screenWidth * waveWidth * sin;
            rotTurret = new Vector3(rotTurret.z, 0f, 0f);
            turret.transform.up = turret.transform.position - rotTurret;
        } 
        else {
            Vector2 direction = turret.transform.position - tankPosition;            
            turret.transform.up = direction;

        }

    }


    public virtual void EnemyAttack()
    { 
        if(distanceTank.magnitude <= searchDistance) {
            RaycastHit2D attack = Physics2D.Raycast(rayPos.transform.position, distanceTank, searchDistance);            
            if(attack.collider.CompareTag("Player") && Time.time > time) {

                Vector3 turretPos = turret.transform.localPosition + Vector3.down * 2f;
                Vector3 shellPos = turret.transform.TransformPoint(turretPos);
                shellEnemy = Instantiate(shellEnemyPrefab, shellPos, turret.transform.rotation);

                float chanseHit = Random.value;
                if(chanseHit >= accuracy / 100f) {
                    shellEnemy.transform.Rotate(0f, 0f, Random.Range(-scatter, scatter));                    
                } else shellEnemy.transform.rotation = turret.rotation;

                Transform turretTransform = turret.transform;
                GameObject fireGO = Instantiate(firePrefab, shellPos, turret.rotation, turretTransform);
                fireGO.transform.Rotate(turret.rotation.x, turret.rotation.y, turret.rotation.z + 180f);                
                Destroy(fireGO.gameObject, 0.15f);

                time = Time.time;
            } 
        } 
        
    }


    public void EnemyDestroy()
    {
        destroy = GameManager.S.restart;

        if(enemyHP <= 0) {            
            Destroy(gameObject);
        }

        if(destroy) {
            Destroy(gameObject);
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
            

    }


}
