using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

//250

public class MainAI : MonoBehaviour
{
    public delegate void Fire(bool fire);
    public event Fire fireEvent;

    [Header("Вручную")]    
    public int enemyHP = 100;
    public float searchDistance = 50f;    
    public float speedRotTurret = 1f;
    public float fireRate;
    public float accuracy = 80f;
    public float scatter = 20f;
    public GameObject shellEnemyPrefab;
    //public HPBarEnemy hpBarEnemy;
    public GameObject firePrefab;
    public GameObject rayPos;

    [Header("Автоматически")]
    [HideInInspector] public int playerDamage;
    [HideInInspector] public float time;
    [HideInInspector] public Transform turret;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Tank tank;
    [HideInInspector] public Vector3 tankPosition;
    [HideInInspector] public Vector3 distanceTank;
    [HideInInspector] public bool moveOn = false;
    //[HideInInspector] public bool destroy;
    [HideInInspector] public bool playerFound = false;
    [HideInInspector] public bool enemyManeuver = false;
    [HideInInspector] public bool enemyEscape = false;

    GameObject lastTrigger = null;
    float rayTime, fireTime, destTime;


    public virtual void Awake()
    {        
        turret = gameObject.transform.GetChild(0);        
        agent = GetComponent<NavMeshAgent>();        
        tank = Tank.S;
    }

    public virtual void Start()
    {
        destTime = rayTime = time = Time.time;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(DistanceTank());
        StartCoroutine(EnemyAttack());
    }

    public virtual void FixedUpdate()
    {
        if(rayTime < 1f) 
            playerDamage = tank.playerDamage;
        
        if(Time.time > rayTime + 1f) {
            FindPlayer();
            rayTime = Time.time;
        }
        
        if(Time.time > fireTime + fireRate) {
            EnemyAttack();
            fireTime = Time.time;
        }

        EnemyMoving();
        DistanceTank();
        EnemyManeuver();
        RotateTurret();        
        EnemyDestroy();
        //EnemyEscape();     
        //FindWay();
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
                return;
            }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellPlayer")) {
            enemyHP -= playerDamage;
            //hpBarEnemy.ScaleBar();
            Destroy(gameObject);
            GameObject explo = Instantiate(firePrefab, transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
    }


    IEnumerator DistanceTank()
    {
        if(tank) {
            tankPosition = tank.transform.position;
        } else {
            Debug.Log("Объект танк не обнаружен!");
            yield return Vector3.zero;
        }
        distanceTank = tankPosition - transform.position;
        //Debug.Log(distanceTank.magnitude);
        yield return distanceTank;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DistanceTank());
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

        Collider[] hitCollider = Physics.OverlapSphere(circlePos, searchDistance, layerMask);
        foreach(Collider alies in hitCollider) {
            if(playerFound && alies.CompareTag("Enemy")) {
                alies.GetComponent<MainAI>().playerFound = true;
            }
        }
        hitCollider = null;
    }


    private void FindWay()
    {
        Vector3 enemyPos = transform.localPosition - transform.forward * 2f;
        Vector2 rayPos = new Vector2(enemyPos.x, enemyPos.y);
        RaycastHit2D way = Physics2D.Raycast(rayPos, -transform.forward, 2f);
        if(way) {
            
        }

    }


    public virtual void EnemyMoving()
    {
        if(distanceTank.magnitude <= searchDistance || playerFound) {
            if(enemyEscape || enemyManeuver) {
                return;
            }
            
            if(agent.enabled && Time.time > destTime + 1f) {
                agent.SetDestination(tankPosition);
                destTime = Time.time;
            }
            Vector3 dest = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
            Vector3 direction = dest - transform.position;
            float step = Time.fixedDeltaTime * 2f;
            transform.forward = Vector3.Lerp(transform.forward, direction, step);
        }
    }


    private void EnemyEscape()
    {
        if(enemyHP <= 50) {
            enemyEscape = true;


            
        } else enemyEscape = false;
    }


    private void EnemyManeuver()
    {
        if(distanceTank.sqrMagnitude <= 40f) {
            enemyManeuver = true;
            agent.velocity = Vector3.zero;            
        } else enemyManeuver = false;
    }


    public virtual void RotateTurret()
    {
        if(distanceTank.magnitude >= searchDistance) {
            turret.transform.forward = transform.forward;
        } 
        else {
            Vector3 target = new Vector3(tankPosition.x, turret.position.y, tankPosition.z);
            Vector3 direction = target - turret.position;
            turret.transform.forward = direction;
        }
    }


    public virtual IEnumerator EnemyAttack()
    {
        
        if(distanceTank.magnitude <= searchDistance) {

            RaycastHit hit;        
            
            if(Physics.Raycast(rayPos.transform.position, distanceTank, out hit, searchDistance)){
                if(hit.collider.CompareTag("Player")) {

                    Vector3 turretPos = turret.position - turret.forward * 3f - turret.transform.up * 0.2f;  
                    GameObject shellEnemy = Instantiate(shellEnemyPrefab, turretPos, turret.transform.rotation);
                    fireEvent?.Invoke(true);
                    //fireEvent?.Invoke(false);

                    float chanseHit = Random.value;
                    if(chanseHit >= accuracy / 100f) {
                        shellEnemy.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);                    
                    } else shellEnemy.transform.rotation = turret.rotation;
                } 
            }

        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(EnemyAttack());        
    }


    public void EnemyDestroy()
    {
        if(enemyHP <= 0) {
            agent.enabled = false;
            Destroy(gameObject);
            GameObject explo = Instantiate(firePrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }

        //if(destroy) {
        //    Destroy(gameObject);

        //}
    }

}
