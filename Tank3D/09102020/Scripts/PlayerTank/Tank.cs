using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 180

public class Tank : MonoBehaviour
{
    static public Tank S;
    [HideInInspector] public UnityEvent<bool> eventMovingAudio;
    [HideInInspector] public UnityEvent<bool> eventRotateAudio;

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
    [HideInInspector] public Transform turret;
    [HideInInspector] public Transform chassis;
    [HideInInspector] public Transform shield;
    [HideInInspector] public float moveX, moveY;
    [HideInInspector] public Vector2 leftJoy;
    [HideInInspector] public Vector2 rightJoy;
    [HideInInspector] public int maxPlayerHP;
    [HideInInspector] public int maxPlayerShield;

    bool reload = false;
    new Camera camera;
    public GameManager gameManager;
    GarageManager garageManager;
    UIManager uiManager;
    LoadParametersTank load;


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
        chassis = gameObject.transform.GetChild(1);
        shield = gameObject.transform.GetChild(3);

        maxPlayerHP = playerHP;
        maxPlayerShield = 100;
    }

    void Start()
    {
        if(LoadParametersTank.S)
            LoadParameters();
        else Debug.Log("LoadParametersTank не найден.");

        uiManager = UIManager.S;

        if(uiManager.androidRuntime)
            aim.SetActive(true);

        if(eventMovingAudio == null)
            eventMovingAudio = new UnityEvent<bool>();

        if(eventRotateAudio == null)
            eventRotateAudio = new UnityEvent<bool>();
    }

    void FixedUpdate()
    {
        Movement();
        RotateTurret();
        if(Input.GetMouseButton(0) && !uiManager.androidRuntime) {
            Fire();
        }

        //ShieldEnabled();
        PlayerDestroy();
    }

    private void LoadParameters()
    {        
        load = LoadParametersTank.S;

        playerHP = load.playerHp;
        playerShield = load.playerShield;
        playerDamage = load.playerDamage;
        fireRate = load.fireRate;
        scatter = load.scatter;
        accuracy = load.accuracy;
        speedMove = load.speedMove;
        speedRotate = load.speedRotate;
        speedRotateTurret = load.speedRotateTurret;

    }

    private void Movement()
    {
        if(uiManager.androidRuntime && !uiManager.winRuntime) {
            leftJoy = UIManager.S.leftJoy;
            moveX = leftJoy.x * speedRotate * Time.deltaTime * 1.1f;
            moveY = leftJoy.y * speedMove * Time.deltaTime * 1.1f;
        } else {
            moveX = Input.GetAxis("Horizontal") * speedRotate * Time.deltaTime * 1.1f;
            moveY = Input.GetAxis("Vertical") * speedMove * Time.deltaTime;
        }

        rigidbody.AddForce(transform.forward * moveY, mode: ForceMode.Impulse);
        rigidbody.AddTorque(transform.up * moveX, mode: ForceMode.Impulse);

        //rigidbody.MoveRotation(Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y * moveX, transform.eulerAngles.z));

        if(Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f || leftJoy.x > 0f || leftJoy.y > 0f)
            eventMovingAudio.Invoke(true);
        else eventMovingAudio.Invoke(false);

    }

    private void RotateTurret()
    {
        float speed = Time.fixedDeltaTime;
        speed += Time.fixedDeltaTime;
        Vector3 dist = Vector3.zero;

        if(uiManager.winRuntime) {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) {
                dist = hit.point - turret.transform.position;
                Quaternion rot = Quaternion.LookRotation(dist);
                turret.transform.rotation = Quaternion.Lerp(turret.rotation, rot, speedRotateTurret * Time.fixedDeltaTime / 1f);
            }

            Quaternion turretRot = turret.rotation;
            float clampX = Mathf.Clamp(turretRot.x, 0f, 0f);
            float clampZ = Mathf.Clamp(turretRot.z, 0f, 0f);
            turretRot.x = clampX;
            turretRot.z = clampZ;
            turret.rotation = turretRot;
        }

        if(uiManager.androidRuntime) {
            rightJoy.x = UIManager.S.rigthFJ.Horizontal;
            turret.eulerAngles = new Vector3(0f, turret.eulerAngles.y + rightJoy.x * Time.fixedDeltaTime * speedRotateTurret * 4f, 0f);
        }

        //if(rightJoy.x > 0.05f || dist.sqrMagnitude > 50f)
        //    eventRotateAudio.Invoke(true);
        //else eventRotateAudio.Invoke(false);
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
            UpdateStatsUI();
            //float scale = (float)playerShield / maxPlayerShield;
            //uiManager.ShieldStripScale(scale);
        } else {
            playerHP -= damage;
            UpdateStatsUI();
            //float scale = (float)playerHP / maxPlayerHP;
            //uiManager.HpStripScale(scale);
        }
    }

    public void UpdateStatsUI()
    {
        uiManager.HpStripScale((float)playerHP / (float)maxPlayerHP);
        uiManager.ShieldStripScale((float)playerShield / (float)maxPlayerShield);

        if(playerShield > 0)
            shield.gameObject.SetActive(true);
        else shield.gameObject.SetActive(false);
    }

    void ShieldEnabled()
    {
        //Debug.Log((float)(playerShield / maxPlayerShield));
        //uiManager.ShieldStripScale((float)playerShield / (float)maxPlayerShield);

        if(uiManager.shieldStrip.transform.localScale.x != (float)playerShield / (float)maxPlayerShield) {
            uiManager.ShieldStripScale((float)playerShield / (float)maxPlayerShield);
        }
    }

}
