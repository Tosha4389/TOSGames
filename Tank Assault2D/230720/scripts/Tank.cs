using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

// 50

public class Tank : MonoBehaviour
{
    static public Tank S;

    [Header("Вручную:")]
    public float speedMove;
    public float speedRotate;
    public float speedRotateTurret;
    public GameObject shellTankPrefab;
    public float fireRate = 5f;


    [Header("Автоматически:")]    
    public Transform turret;
    public BorderCheck borderCheck;
    public Shell shell;

    private float xLimit;
    private float yLimit;    
    private IEnumerator coruntine;
    private bool reload = false;
    

    private void Awake()
    {            
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("Tank.Awake() - создан второй Tank");
            Destroy(gameObject);
        }

        borderCheck = GetComponent<BorderCheck>();
    }

    void Start()
    {
        turret = gameObject.transform.GetChild(0);
        coruntine = Reload(fireRate);
        
    }
        
    void FixedUpdate()
    {
        Movement();
        RotateTurret();        
        Fire();
        
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 positionTank = new Vector3(0, moveY, 0);
        transform.Translate(positionTank * speedMove * Time.fixedDeltaTime);
                
        xLimit = Mathf.Clamp(transform.position.x, -borderCheck.screenWidth + borderCheck.size, borderCheck.screenWidth - borderCheck.size);
        yLimit = Mathf.Clamp(transform.position.y, 0f + borderCheck.size, transform.position.y);
        transform.position = new Vector3(xLimit, yLimit, transform.position.z);
                
        // Реверс поворота при движении задним ходом
        if(moveY < 0) {
            transform.Rotate(0, 0, -moveX * speedRotate * Time.fixedDeltaTime);
        } else {
            transform.Rotate(0, 0, moveX * speedRotate * Time.fixedDeltaTime);
        }        


    }

    private void RotateTurret()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();
        float rotateTurret = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        turret.transform.rotation = Quaternion.Euler(0f, 0f, rotateTurret - 90f);      

    }

    public void Fire()
    {
        if(Input.GetMouseButton(0) && reload == false) {
            Instantiate(shellTankPrefab); 
            StartCoroutine(Reload(fireRate));
            
        }

        shellTankPrefab.transform.position = turret.transform.position;
        shellTankPrefab.transform.rotation = turret.transform.rotation; 
    }

    public IEnumerator Reload(float time)
    {
        while(true) {
            reload = true;                      
            yield return new WaitForSeconds(fireRate);
            reload = false;
            break;
        }       

    }


}
