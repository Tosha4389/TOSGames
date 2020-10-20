using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class MovementTank : MonoBehaviour, IState
{
    [SerializeField] GameObject rTrack;
    [SerializeField] GameObject lTrack;
    [SerializeField] ParticleSystem exhaustPS;
    Material rMat;
    Material lMat;
    NavMeshAgent agent;
    bool isPause = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rMat = rTrack.GetComponent<MeshRenderer>().material;
        lMat = lTrack.GetComponent<MeshRenderer>().material;
    }

    public void Movement(Vector3 destination)
    {
        if(agent.enabled && !isPause) {
            StartCoroutine(GetPause());
            agent.SetDestination(destination);
        }       

        Vector3 steeringDest = new Vector3(agent.steeringTarget.x, transform.position.y, agent.steeringTarget.z);
        Vector3 direction = steeringDest - transform.position;
        float step = Time.deltaTime * 3f;
        transform.forward = Vector3.RotateTowards(transform.forward, direction, step, 0.0f);
        EffectsMovement();
    }

    IEnumerator GetPause()
    {
        isPause = true;
        yield return new WaitForSeconds(1f);
        isPause = false;
    }

    void EffectsMovement()
    {
        if(agent.velocity.x > 0.0005f || agent.velocity.z > 0.0005f) {
            rMat.mainTextureOffset += Vector2.right * 0.1f;
            lMat.mainTextureOffset += Vector2.right * 0.1f;
            exhaustPS.Play(true);
        } else {
            rMat.mainTextureOffset = Vector2.zero;
            lMat.mainTextureOffset = Vector2.zero;
            exhaustPS.Stop(true);
        }
    }
}
