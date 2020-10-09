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
    public GameObject firePrefab;
    public GameObject rayPos;

    [Header("Автоматически")]
    [HideInInspector] public int playerDamage;
    [HideInInspector] public float time;
    [HideInInspector] public Transform turret;
    [HideInInspector] public Transform chassis;
    [HideInInspector] public Rigidbody rigidbody;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Tank tank;
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public Vector3 tankPosition;
    [HideInInspector] public Vector3 distanceTank;
    [HideInInspector] public bool moveOn = false;
    [HideInInspector] public bool playerFound = false;
    [HideInInspector] public bool enemyManeuver = false;
    [HideInInspector] public bool enemyEscape = false;

    AudioSource fireAudio;
    GameObject lastTrigger = null;
    float destTime;


    public virtual void Awake()
    {        
        turret = gameObject.transform.GetChild(0);
        chassis = gameObject.transform.GetChild(1);
        rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();        
        tank = Tank.S;
        fireAudio = GetComponent<AudioSource>();

    }

    public virtual void Start()
    {
        gameManager = GameManager.S;
        destTime = Time.time;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(Initialization());
        StartCoroutine(DistanceTank());
        StartCoroutine(EnemyAttack());
        StartCoroutine(FindPlayer());
    }

    public virtual void FixedUpdate()
    {
        EnemyMoving();
        DistanceTank();
        EnemyManeuver();
        RotateTurret();        
        EnemyDestroy();
        //EnemyEscape();

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

    IEnumerator Initialization()
    {
        yield return new WaitForSeconds(3);
        playerDamage = tank.playerDamage;
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


    IEnumerator FindPlayer()
    {        
        if(distanceTank.magnitude <= searchDistance) {
            playerFound = true;
        }        

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
        yield return new WaitForSeconds(1);
        StartCoroutine(FindPlayer());
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
            float step = Time.deltaTime * 3f;
            transform.forward = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
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
                    fireAudio.Play();
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
            gameManager.CreateBonus(transform.position);
            Destroy(gameObject);
            GameObject explo = Instantiate(firePrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
    }

}
