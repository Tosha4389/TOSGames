using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 180

public class Tank : MonoBehaviour
{
    static public Tank S;

    [Header("Вручную:")]
    public int playerHP;
    public int playerDamage;
    public float fireRate;
    public float scatter;
    public float accuracy;
    public float speedMove;
    public float speedRotate;
    public float speedRotateTurret;
    public bool godMode = false;
    public GameObject shellTankPrefab;
    public GameObject explosionPrefab;
    public GameObject cursor;

    [Header("Автоматически:")]    
    public BorderCheck borderCheck;
    public Shell shell;
    public new Rigidbody2D rigidbody;
    public HPBar hpBar;

    private float xLimit;
    private float yLimit;    
    private IEnumerator coruntine;
    private bool reload = false;
    private new Camera camera;
    private Transform turret;
    private Transform chassis;
    private Transform gun;
    private SpriteRenderer turretRend;
    private SpriteRenderer chassisRend;
    private SpriteRenderer gunRend;
    private GameObject lastTrigger = null;
    private GarageManager garageManager;
    private LoadParametersTank load;

    private void Awake()
    {            
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("Tank.Awake() - создан второй Tank");
            Destroy(gameObject);
        }

        camera = Camera.main;
        borderCheck = GetComponent<BorderCheck>();
        rigidbody = GetComponent<Rigidbody2D>();
        garageManager = GarageManager.S;
    }

    void Start()
    {
        turret = gameObject.transform.GetChild(0);
        gun = turret.transform.GetChild(0);
        chassis = gameObject.transform.GetChild(1);

        turretRend = turret.GetComponent<SpriteRenderer>();
        gunRend = gun.GetComponent<SpriteRenderer>();
        chassisRend = chassis.GetComponent<SpriteRenderer>();

        if(LoadParametersTank.S)
            LoadParameters();
        else Debug.Log("LoadParametersTank не найден.");

    }
        
    void FixedUpdate()
    {
        Movement();
        RotateTurret();        
        Fire();
        PlayerDestroy();
    }

    private void LoadParameters()
    {
        
        load = LoadParametersTank.S;

        playerDamage = load.playerDamageLoad;
        fireRate = load.fireRateLoad;
        scatter = load.scatterLoad;
        accuracy = load.accuracyLoad;
        speedMove = load.speedMoveLoad;
        speedRotate = load.speedRotateLoad;
        speedRotateTurret = load.speedRotateTurretLoad;

        turretRend.sprite = load.tankSprites[0];
        gunRend.sprite = load.tankSprites[1];
        chassisRend.sprite = load.tankSprites[2];

        gun.transform.localPosition = load.gunPos;
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal") * speedRotate * 3f * Time.fixedDeltaTime;
        float moveY = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;

        rigidbody.AddRelativeForce(Vector3.up * moveY, mode: ForceMode2D.Impulse);
        rigidbody.AddTorque(-moveX, mode: ForceMode2D.Impulse);

        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);


    }

    private void RotateTurret()
    {
        float speed = Time.fixedDeltaTime;
        speed += Time.fixedDeltaTime;

        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        Vector3 direction = mousePosition - turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float step = speedRotateTurret * Time.fixedDeltaTime * 5f;
        turret.rotation = Quaternion.RotateTowards(turret.rotation, Quaternion.Euler(0f, 0f, angle - 90f), step);
        
        //cursor.transform.position = new Vector3(mousePosition.x, mousePosition.y, -1f);

    }

    public void Fire()
    {  
        if(Input.GetMouseButton(0) && reload == false) {
            Vector3 turretPos = turret.transform.localPosition + Vector3.up * 2f;
            Vector3 shellPos = turret.transform.TransformPoint(turretPos);
            GameObject shell = Instantiate(shellTankPrefab, shellPos, turret.rotation);
            float chanseHit = Random.value;

            if(chanseHit > accuracy / 100f) {
                shell.transform.Rotate(0f, 0f, Random.Range(-scatter, scatter));
                //Debug.Log(chanseHit);
            } else shell.transform.rotation = turret.transform.rotation;

            StartCoroutine(Reload(fireRate));
        }

    }

    public IEnumerator Reload(float time)
    {
        while(true) {
            reload = true;                      
            yield return new WaitForSeconds(time);
            reload = false;
            break;
        }
    }

    private void PlayerDestroy()
    {
        if(playerHP <= 0 && !godMode) {
            GameManager.S.GameOver();
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform rootTransform = other.gameObject.transform.root;
        GameObject gameObject = rootTransform.gameObject;

        if(lastTrigger == gameObject) {
            return;
        }
        lastTrigger = gameObject;

        if(other.gameObject.CompareTag("ShellEnemy")) {
            shell = other.GetComponent<ShellEnemy>();
            playerHP -= shell.enemyDamage;
            hpBar.ScaleBar();
            Destroy(other.gameObject);
            GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
        }
    }



}
