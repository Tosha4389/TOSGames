using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesParticleSystem : MonoBehaviour
{
    [SerializeField] float timeAction;
    [SerializeField] float delay;
    [SerializeField] ParticleSystem particleSys;
    BoxCollider collid;

    private void Awake()
    {
        collid = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(DelayAction());
    }

    IEnumerator DelayAction()
    {
        particleSys.Play();
        collid.enabled = true;

        yield return new WaitForSeconds(timeAction);

        particleSys.Stop();
        collid.enabled = false;

        yield return new WaitForSeconds(delay);

        StartCoroutine(DelayAction());
    }
}
