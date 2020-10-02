using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 180

public class Tank : MonoBehaviour
{
    static public Tank S;

    [Header("Вручную:")]
    public int playerHP;
    public int playerShield;
    public int playerDamage;
    public float fireRate;
    public float scatter;
    public float accuracy;
    public float speedMove;
    public float speedRotate;
    public float speedRotateTurret;
    public bool godMode = false;
    public GameObject shellTankPrefab;
    public GameObject firePrefab;
    public GameObject aim;

    [HideInInspector] public Shell shell;
    [HideInInspector] public new Rigidbody rigidbody;
    [HideInInspector] public HPBar hpBar;
    [HideInInspector] public float moveX, moveY;

    int maxPlayerHP;
    int maxPlayerShield;
    bool reload = false;
    new Camera camera;
    Transform turret;
    //Transform chassis;
    //GameObject lastTrigger = null;
    GarageManager garageManager;
    UIManager uIManager;
    LoadParametersTank load;
    Vector2 leftJoy;
    Vector2 rightJoy;

    private void Awake()
    {            
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("Tank.Awake() - создан второй Tank");
            Destroy(gameObject);
        }

        camera = Camera.main;        
        rigidbody = GetComponent<Rigidbody>();
        garageManager = GarageManager.S;
        turret = gameObject.transform.GetChild(0);
        //chassis = gameObject.transform.GetChild(1);

        maxPlayerHP = playerHP;
        maxPlayerShield = playerShield;
    }

    void Start()
    {
        if(LoadParametersTank.S)
            LoadParameters();
        else Debug.Log("LoadParametersTank не найден.");
        
        uIManager = UIManager.S;

        if(uIManager.androidRuntime)
            aim.SetActive(true);


    }
        
    void FixedUpdate()
    {
        Movement();
        RotateTurret();
        if(Input.GetMouseButton(0) && !uIManager.androidRuntime)
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
        if(uIManager.androidRuntime && !uIManager.winRuntime) {
            leftJoy = UIManager.S.leftJoy;
            moveX = leftJoy.x * speedRotate * Time.fixedDeltaTime * 1.1f;
            moveY = leftJoy.y * speedMove * Time.fixedDeltaTime * 1.1f;
        } else {
            moveX = Input.GetAxis("Horizontal") * speedRotate * Time.fixedDeltaTime;
            moveY = Input.GetAxis("Vertical") * speedMove * Time.fixedDeltaTime;
        }

        rigidbody.AddForce(transform.forward * moveY, mode: ForceMode.Impulse);
        rigidbody.AddTorque(transform.up * moveX, mode: ForceMode.Impulse);
    }

    private void RotateTurret()
    {
        float speed = Time.fixedDeltaTime;
        speed += Time.fixedDeltaTime;

        if(uIManager.winRuntime) {

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                Vector3 dist = hit.point - turret.transform.position;
                Quaternion rot = Quaternion.LookRotation(dist);
                turret.transform.rotation = Quaternion.Lerp(turret.rotation, rot, speedRotateTurret * Time.fixedDeltaTime / 1f);

                //if(clamp.sqrMagnitude > 6f) {
                //    turret.transform.LookAt(hit.point);
                //}
            }
            Quaternion turretRot = turret.rotation;

            float clampX = Mathf.Clamp(turretRot.x, 0f, 0f);
            float clampZ = Mathf.Clamp(turretRot.z, 0f, 0f);
            turretRot.x = clampX;
            turretRot.z = clampZ;

            turret.rotation = turretRot;
        }

        if(uIManager.androidRuntime) {
            rightJoy.x = UIManager.S.rigthFJ.Horizontal;
            turret.eulerAngles = new Vector3(0f, turret.eulerAngles.y + rightJoy.x * Time.fixedDeltaTime * speedRotateTurret * 4f, 0f);

            //rightJoy = UIManager.S.rightJoy;
            //float angle = Mathf.Atan2(rightJoy.y, rightJoy.x) * Mathf.Rad2Deg;
            //if(angle != 0) {
            //    angle -= 90f;
            //}
            //float step = speedRotateTurret * Time.fixedDeltaTime * 3f;
            //turret.rotation = Quaternion.RotateTowards(turret.rotation, Quaternion.Euler(0f, -angle, 0f), step);
        }


    }

    public void Fire()
    { 
        if(reload == false) {
            Vector3 turretPos = turret.transform.position + turret.transform.forward * 3f + turret.transform.up * 0.25f;            
            GameObject shell = Instantiate(shellTankPrefab, turretPos, turret.rotation);
            Vector3 exploPos = turret.transform.position + turret.transform.forward * 3f + turret.transform.up * 0.25f;            
            GameObject explo = Instantiate(firePrefab, exploPos, turret.rotation);
            Destroy(explo, 4f);

            float chanseHit = Random.value;
            if(chanseHit > accuracy / 100f) {
               shell.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);                
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
            GameObject explo = Instantiate(firePrefab, gameObject.transform.position, Quaternion.identity);
            Destroy(explo, 1f);
            Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void HitPlayerTank(int damage)
    {
        if(playerShield > 0) {
            playerShield -= damage;
            float scale = (float)playerShield / maxPlayerShield;
            uIManager.ShieldStripScale(scale);
        } else {
            playerHP -= damage;
            float scale = (float)playerHP / maxPlayerHP;
            uIManager.HpStripScale(scale);
        }
    }

}
