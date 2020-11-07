using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StationEvent : UnityEvent
{

}

public class TrainController : MonoBehaviour
{
    [SerializeField] StationEvent trainEnter;
    [SerializeField] StationEvent trainExit;

    Movable movable;
    TrainSounds trainSounds;
    Transform train;

    void Awake()
    {
        movable = GetComponent<Movable>();
        trainSounds = GetComponent<TrainSounds>();
        train = GetComponent<Transform>();

        if(trainEnter == null)
            trainEnter = new StationEvent();

        if(trainExit == null)
            trainExit = new StationEvent();
    }

    void FixedUpdate()
    {
        movable.Move();
    }

    public void PlayBeep()
    {
        trainSounds.Playbeep();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Station")) {
            StartCoroutine(CountVelocity());
        }
    }

    IEnumerator CountVelocity()
    {
        float time = 0.3f;
        float startZ = train.position.z;
        yield return new WaitForSeconds(time);
        float finishZ = train.position.z;
        float speed = (Mathf.Abs(finishZ) - Mathf.Abs(startZ)) / time;

        if(Mathf.Abs(speed) < 0.001f)
            trainEnter.Invoke();
        else trainExit.Invoke();
        StartCoroutine(CountVelocity());
    }
}
