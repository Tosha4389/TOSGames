using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagable
{
    [SerializeField] int hp;
    [SerializeField] ParticleSystem wreck;
    [SerializeField] ParticleSystem smoke;

    MeshRenderer mesh;
    BoxCollider collider;
    AudioSource wallAudio;

    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        wallAudio = GetComponent<AudioSource>();
    }

    public void DecreaseValue(int damage)
    {
        hp -= damage;
        if(hp <= 0)
            DestroyGO();
    }

    public void DestroyGO()
    {
        wreck.Play(true);
        smoke.Play(true);

        if(!gameObject.CompareTag("NoDestroy")) {
            mesh.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, 2f);
            if(!wallAudio.isPlaying)
                wallAudio.Play();
        }

        if(gameObject.CompareTag("NoDestroy") && !wallAudio.isPlaying) {
            wallAudio.Play();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        DestroyGO();
    }
}
