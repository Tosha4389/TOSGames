using System.Collections;
using UnityEngine;

public class ShootingTank : MonoBehaviour, IShooting
{
    [SerializeField] float fireRate = 1f;
    [SerializeField] float accuracy = 80f;
    [SerializeField] float scatter = 20f;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] ParticleSystem firePS;
    [SerializeField] ParticleSystem smokePS;

    Transform turret;
    bool isReload = false;

    void Awake()
    {
        turret = gameObject.transform.GetChild(0);
    }  
    
    public void Shooting()
    {        
        if(!isReload) {
            Vector3 position = turret.position + turret.forward * 3f - turret.transform.up * 0.7f;
            GameObject shellEnemy = Instantiate(shellPrefab, position, turret.transform.rotation);

            float chanseHit = Random.value;
            if(chanseHit >= accuracy / 100f) {
                shellEnemy.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);
            } else shellEnemy.transform.rotation = turret.rotation;

            StartCoroutine(ReloadShell());
        }
    }

    IEnumerator ReloadShell()
    {
        isReload = true;
        smokePS.Play(true);
        firePS.Play(true);
        yield return new WaitForSeconds(fireRate);
        isReload = false;
        smokePS.Stop(true);
        firePS.Stop(true);
    }
}
