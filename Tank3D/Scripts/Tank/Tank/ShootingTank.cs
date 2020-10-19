using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTank : MonoBehaviour, IShooting
{
    public float fireRate { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float scatter { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float accuracy { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public GameObject shellPrefab => throw new System.NotImplementedException();

    public ParticleSystem firePS => throw new System.NotImplementedException();

    public ParticleSystem smoke => throw new System.NotImplementedException();

    public void Shooting()
    {
        throw new System.NotImplementedException();
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
