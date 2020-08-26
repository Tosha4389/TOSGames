using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;

//250

[Serializable]
public class Gun
{
    public int id;
    public string name;
    public int damage;
    public float fireRate;
    public float scatter;
    public float accuracy;

    public Gun(int i, string n, int d, float fR, float sc, float ac)
    {
        id = i;
        name = n;

        fireRate = fR;
        scatter = sc;
        accuracy = ac;
    }
}

[Serializable]
public class Turret
{
    public int id;
    public string name;
    public float speedRot;

    public Turret(int i, string n, float sR)
    {
        id = i;
        name = n;
        speedRot = sR;
    }
}

[Serializable]
public class Chassis
{
    public int id;
    public string name;
    public float speedMove;
    public float speedRot;

    public Chassis(int i, string n, float sM, float sR)
    {
        id = i;
        name = n;
        speedMove = sM;
        speedRot = sR;
    }
}

public class GarageManager : MonoBehaviour
{
    [Header("Сохранение файла")]
    [SerializeField] private string filePath;
    [SerializeField] private string gunFileName = "gun.json";
    [SerializeField] private string turretFileName = "turrets.json";
    [SerializeField] private string chassisFileName = "chassis.json";

    [SerializeField] private List<Gun> gunUpd;
    [SerializeField] private List<Turret> turretUpd;
    [SerializeField] private List<Chassis> chassisUpd;

    [Header("Списки спрайтов")]
    public List<Sprite> gunSprites;
    public List<Sprite> turretSprites;
    public List<Sprite> chassisSprites;

    public LoadParametersTank loadParam;
    public GameObject tankTurret;
    public GameObject tankGun;
    public GameObject tankChassis;

    private SpriteRenderer turretRend;
    private SpriteRenderer gunRend;
    private SpriteRenderer chassisRend;

    Vector3 gunPosStart;
    Vector3 gunPos;

    public Text debug;

    static public GarageManager S;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GarageManager.Awake() - создан второй GarageManager");
            Destroy(gameObject);
        }

        Cursor.visible = true;
        
        gunFileName = "gun.json";
        turretFileName = "turrets.json";
        chassisFileName = "chassis.json";

        //SaveToFile(gunFileName, gunUpd);
        //SaveToFile(turretFileName, turretUpd);
        //SaveToFile(chassisFileName, chassisUpd);
        LoadFromFile(gunFileName, gunUpd);
        LoadFromFile(turretFileName, turretUpd);
        LoadFromFile(chassisFileName, chassisUpd);

        turretRend = tankTurret.GetComponent<SpriteRenderer>();
        gunRend = tankGun.GetComponent<SpriteRenderer>();
        chassisRend = tankChassis.GetComponent<SpriteRenderer>();

        gunPosStart = gunPos = tankGun.transform.localPosition;
    }

    private void Start()
    {
        loadParam = LoadParametersTank.S;
        StartParameters();

        turretRend.sprite = loadParam.tankSprites[0];
        gunRend.sprite = loadParam.tankSprites[1];
        chassisRend.sprite = loadParam.tankSprites[2];
    }

    private void StartParameters()
    {
        turretRend.sprite = loadParam.tankSprites[0];
        gunRend.sprite = loadParam.tankSprites[1];
        chassisRend.sprite = loadParam.tankSprites[2];
        tankGun.transform.localPosition = loadParam.gunPos;

    }

    public void ActiveUpgradeTurret(int id)
    {       
        loadParam.speedRotateTurretLoad = turretUpd[id].speedRot;
        turretRend.sprite = loadParam.tankSprites[0] = turretSprites[id];        
    }

    public void ActiveUpgradeGun(int id)
    {
        loadParam.playerDamageLoad = gunUpd[id].damage;
        loadParam.fireRateLoad = gunUpd[id].fireRate;
        loadParam.scatterLoad = gunUpd[id].scatter;
        loadParam.accuracyLoad = gunUpd[id].accuracy;
        gunRend.sprite = loadParam.tankSprites[1] = gunSprites[id];

        switch(id) {
            case 0:                
                gunPos.y = 0.62f;
                tankGun.transform.localPosition = gunPos;
                break;
            case 1:
                tankGun.transform.localPosition = gunPos = gunPosStart;                
                break;
            default:
                break;
        }

        loadParam.gunPos = gunPos;        
    }

    public void ActiveUpgradeChassis(int id)
    {
        loadParam.speedMoveLoad = chassisUpd[id].speedMove;
        loadParam.speedRotateLoad = chassisUpd[id].speedRot;
        chassisRend.sprite = loadParam.tankSprites[2] = chassisSprites[id];        
    }

    public List<Turret> LoadFromFile(string fileName, List<Turret> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            //debug.text = "ошибка загрузки: файл башень не найден";
            return turretUpd = list;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Turret>();
            list = JsonConvert.DeserializeObject<List<Turret>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка.");
        }

        return turretUpd = list;
    }

    public List<Gun> LoadFromFile(string fileName, List<Gun> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            //debug.text = "ошибка загрузки: файл пушек не найден";
            return gunUpd = list;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Gun>();
            list = JsonConvert.DeserializeObject<List<Gun>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка.");
        }

        return gunUpd = list;
    }

    public List<Chassis> LoadFromFile(string fileName, List<Chassis> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            //debug.text = "ошибка загрузки: файл пушек не найден";
            return chassisUpd = list;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Chassis>();
            list = JsonConvert.DeserializeObject<List<Chassis>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка.");
        }

        return chassisUpd = list;
    }

    public void SaveToFile(string fileName, List<Turret> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        list = new List<Turret>();
        list.Add(new Turret(1, "Дефолтная башня игрока", 40f));
        list.Add(new Turret(2, "Крутая башня игрока", 50f));
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);

        try {
            File.WriteAllText(filePath, json);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }

    public void SaveToFile(string fileName, List<Gun> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        list = new List<Gun>();
        list.Add(new Gun(1, "Дефолтная пушка игрока", 10, 1f, 15f, 80f));
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);

        try {
            File.WriteAllText(filePath, json);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }

    public void SaveToFile(string fileName, List<Chassis> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        list = new List<Chassis>();
        list.Add(new Chassis(1, "Дефолтное шасси игрока", 12f, 50f));
        list.Add(new Chassis(2, "Крутое шасси игрока", 14f, 60f));
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);

        try {
            File.WriteAllText(filePath, json);
        }
        catch(Exception) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }



}
