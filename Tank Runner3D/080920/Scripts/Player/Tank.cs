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
    //public GameObject joyR;
    //public GameObject joyL;

    [Header("Автоматически:")]    
    public BorderCheck borderCheck;
    public Shell shell;
    public new Rigidbody rigidbody;
    public HPBar hpBar;
    FixedJoystick rigthFJ;
    FixedJoystick leftFJ;    
    public float moveX, moveY;
 
    private float xLimit;
    private float yLimit;        
    private bool reload = false;
    private new Camera camera;
    private Transform turret;
    private Transform chassis;
    private GameObject lastTrigger = null;
    private GarageManager garageManager;
    private LoadParametersTank load;
    public ParticleSystem firePS;

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
        rigidbody = GetComponent<Rigidbody>();
        garageManager = GarageManager.S;

        turret = gameObject.transform.GetChild(0);        
        chassis = gameObject.transform.GetChild(1);

        firePS = turret.gameObject.GetComponent<ParticleSystem>();

        //rigthFJ = joyR.GetComponent<FixedJoystick>();
        //leftFJ = joyL.GetComponent<FixedJoystick>();
    }

    void Start()
    {

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

    }

    private void Movement()
    {
        //if(Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0) {
        //    moveX = leftFJ.Horizontal * speedRotate * 3f * Time.fixedDeltaTime;
        //    moveY = leftFJ.Vertical * speedMove * Time.fixedDeltaTime;
        //} else {
        //    moveX = Input.GetAxis("Horizontal") * speedRotate * 3f * Time.fixedDeltaTime;
        //    moveY = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;
        //}

        moveX = Input.GetAxis("Horizontal") * speedRotate * Time.fixedDeltaTime;
        moveY = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;

        rigidbody.AddForce(transform.forward * moveY, mode: ForceMode.Impulse);
        rigidbody.AddTorque(transform.up * moveX, mode: ForceMode.Impulse);


        //xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        //yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        //transform.position = new Vector3(xLimit, yLimit, transform.position.z);


    }

    private void RotateTurret()
    {
        float speed = Time.fixedDeltaTime;
        speed += Time.fixedDeltaTime;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)) {            
            turret.transform.LookAt(hit.point);
        }
        Quaternion turretRot = turret.rotation;

        float clampX = Mathf.Clamp(turretRot.x, 0f, 0f);
        float clampZ = Mathf.Clamp(turretRot.z, 0f, 0f);
        turretRot.x = clampX;
        turretRot.z = clampZ;

        turret.rotation = turretRot;


        //Debug.Log(mousePosition);
        //Debug.Log();

        //mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 direction = turret.position - mousePosition;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //float angle = Mathf.Atan2(rigthFJ.Direction.y, rigthFJ.Direction.x) * Mathf.Rad2Deg;
        //if(angle != 0) {
        //    angle -= 90f;
        //}
        //float step = speedRotateTurret * Time.fixedDeltaTime * 7f;
        //turret.rotation = Quaternion.RotateTowards(turret.rotation, Quaternion.Euler(0f, angle, 0f), step);

    }

    public void Fire()
    {  
        if(Input.GetMouseButton(0) && reload == false) {
            Vector3 turretPos = turret.transform.position + turret.transform.forward * 3f + turret.transform.up * 0.25f;
            //Vector3 shellPos = transform.TransformDirection(turretPos);
            GameObject shell = Instantiate(shellTankPrefab, turretPos, turret.rotation);

            firePS.Play(true);

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
            UIManager.S.GameOver();
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
            //GameObject explo = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            //Destroy(explo, 1f);
        }
    }



}
