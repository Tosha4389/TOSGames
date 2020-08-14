using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGarage : MonoBehaviour
{
    public Button gunUpg;
    public Button turretUpg;
    public Button chassissUpg;

    public GameObject gunMenu;
    public GameObject turretMenu;
    public GameObject chassisMenu;


    public void EnableGunMenu()
    {
        turretMenu.SetActive(false);
        chassisMenu.SetActive(false);
        if(gunMenu.activeInHierarchy == false) {
            gunMenu.SetActive(true);

        }
        else gunMenu.SetActive(false);
    }

    public void EnableTurretMenu()
    {
        gunMenu.SetActive(false);
        chassisMenu.SetActive(false);
        if(turretMenu.activeInHierarchy == false) {    
            turretMenu.SetActive(true);            
        }
        else turretMenu.SetActive(false);
    }

    public void EnableChassissMenu()
    {
        gunMenu.SetActive(false);
        turretMenu.SetActive(false);
        if(chassisMenu.activeInHierarchy == false) {

            chassisMenu.SetActive(true);
        }
        else chassisMenu.SetActive(false);
    }

}
