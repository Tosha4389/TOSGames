using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //public float delayDestroy;
    public ParticleSystem wreck; 
    public ParticleSystem smoke;

    MeshRenderer mesh;
    BoxCollider collider;



    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        //mat.SetFloat("Vector1_A4EF9361", 3.1f);

        wreck.Play(true);
        smoke.Play(true);

        if(!gameObject.CompareTag("NoDestroy")) {
            mesh.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, 2f);
        }


        //StartCoroutine(Combustion());
    }

    //IEnumerator Combustion()
    //{
    //    float i = mat.GetFloat("Vector1_A4EF9361");
    //    i += speedComb;
    //    mat.SetFloat("Vector1_A4EF9361", i);
    //    yield return new WaitForSeconds(0.05f);
    //    if(i < 1f)
    //        StartCoroutine(Combustion());
    //    else { 
    //        StopCoroutine(Combustion());
    //        Destroy(gameObject, delayDestroy);
    //    }

    //}
}
