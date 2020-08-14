using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

[Serializable]
public class Gun
{
    public int id;
    public string name;
    public float fireRate;
    public float scatter;
    public float accuracy;

    public Gun(int i, string n, float fR, float sc, float ac)
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


    [Header("Списки спрайтов")]
    public List<Sprite> gunSprites;
    public List<Sprite> turretSprites;
    public List<Sprite> chassisSprites;

    [SerializeField] private List<Gun> gunUpd;
    [SerializeField] private List<Turret> turretUpd;
    [SerializeField] private List<Chassis> chassisUpd;


    static public GarageManager S;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GarageManager.Awake() - создан второй GarageManager");
            //Destroy(gameObject);
        }

        gunFileName = "gun.json";
        turretFileName = "turrets.json";
        chassisFileName = "chassis.json";

        //SaveToFile(gunFileName, gunUpd);
        //SaveToFile(turretFileName, turretUpd);
        //SaveToFile(chassisFileName, chassisUpd);

        LoadFromFile(gunFileName, gunUpd);
        LoadFromFile(turretFileName, turretUpd);
        LoadFromFile(chassisFileName, chassisUpd);
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
        catch(Exception e) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }

    public void LoadFromFile(string fileName, List<Turret> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            return;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Turret>();
            list = JsonConvert.DeserializeObject<List<Turret>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception e) {
            Debug.Log("Возникла ошибка.");
        }
    }

    public void SaveToFile(string fileName, List<Gun> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        list = new List<Gun>();
        list.Add(new Gun(1, "Дефолтная пушка игрока", 1f, 15f, 80f));
        list.Add(new Gun(1, "Крутая пушка игрока", 0.8f, 12f, 90f));
        string json = JsonConvert.SerializeObject(list, Formatting.Indented);

        try {
            File.WriteAllText(filePath, json);
        }
        catch(Exception e) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }

    public void LoadFromFile(string fileName, List<Gun> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            return;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Gun>();
            list = JsonConvert.DeserializeObject<List<Gun>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception e) {
            Debug.Log("Возникла ошибка.");
        }
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
        catch(Exception e) {
            Debug.Log("Возникла ошибка сохранения файла.");
        }
        list.Clear();
    }

    public void LoadFromFile(string fileName, List<Chassis> list)
    {
        string filePath = Path.Combine(Application.dataPath + @"/json/", fileName);
        if(!File.Exists(filePath)) {
            Debug.Log("ошибка загрузки: файл не найден.");
            return;
        }

        try {
            string json = File.ReadAllText(filePath);
            list = new List<Chassis>();
            list = JsonConvert.DeserializeObject<List<Chassis>>(json);

            //Debug.Log(list[0].name);
            //Debug.Log(list[1].name);
        }
        catch(Exception e) {
            Debug.Log("Возникла ошибка.");
        }
    }

}
