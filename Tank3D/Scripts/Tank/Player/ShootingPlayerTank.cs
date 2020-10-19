using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayerTank : MonoBehaviour, IShooting
{
    [SerializeField] float fireRate;
    [SerializeField] float scatter;
    [SerializeField] float accuracy;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] ParticleSystem firePS;
    [SerializeField] ParticleSystem smokePS;

    Transform turret;
    bool reload = false;

    void Awake()
    {
        turret = gameObject.transform.GetChild(0);
    }

    public void Shooting()
    {
        if(reload == false) {
            Vector3 turretPos = turret.transform.position + turret.transform.forward * 3f + turret.transform.up * 0.25f;
            GameObject shell = Instantiate(shellPrefab, turretPos, turret.rotation);

            PlayEffects();

            float chanseHit = Random.value;
            if(chanseHit > accuracy / 100f) {
                shell.transform.Rotate(0f, Random.Range(-scatter, scatter), 0f);
            } else shell.transform.rotation = turret.transform.rotation;

            StartCoroutine(Reload(fireRate));
        }
    }

    public IEnumerator Reload(float time)
    {
        reload = true;
        yield return new WaitForSeconds(time);
        reload = false;
    }

    void PlayEffects()
    {
        firePS.Play(true);
        smokePS.Play(true);
    }   
}
