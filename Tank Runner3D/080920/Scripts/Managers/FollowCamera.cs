using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 10

public class FollowCamera : MonoBehaviour
{
    [Header("Вручную:")]
    public Transform target;
    public float speed = 4f;

    [Header("Автоматически:")]
    public LayerMask maskObstacles;

    Vector3 _position;
    float delta;

    void Awake()
    {
        _position = target.InverseTransformPoint(transform.position);
        delta = speed * Time.fixedDeltaTime / 10f;
    }

    void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        Vector3 currentPosition = target.TransformPoint(_position);
        transform.position = Vector3.Lerp(transform.position, currentPosition, delta);

        Vector3 camPos = new Vector3(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, camPos, delta);

        //Quaternion oldRotation = target.rotation;
        //target.rotation = Quaternion.Euler(0f, oldRotation.eulerAngles.y, 0f);
        //target.rotation = oldRotation;
        //Quaternion currentRotation = Quaternion.LookRotation(target.position - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, Time.fixedDeltaTime);

        RaycastHit hit;
        if(Physics.Raycast(target.position, transform.position - target.position, out hit, Vector3.Distance(transform.position, target.position), maskObstacles)) {
            transform.position = hit.point;
            transform.LookAt(target);
        }
    }

}
