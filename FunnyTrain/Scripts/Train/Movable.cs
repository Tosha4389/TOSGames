using UnityEngine;
using PathCreation;

public class Movable : MonoBehaviour
{
    [SerializeField] float speed = 100f;
    [SerializeField] PathCreator pathNew;

    InputManager inputManager;
    Rigidbody rigid;
    EndOfPathInstruction end;
    float delta;
    float dstTravelled;


    private void Awake()
    {
        delta = speed * Time.fixedDeltaTime;
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputManager = InputManager.S;
    }

    public void Move()
    {
        dstTravelled += delta * inputManager.InputJoystickTrain();
        rigid.MovePosition(pathNew.path.GetPointAtDistance(dstTravelled, end));        
        rigid.MoveRotation(pathNew.path.GetRotationAtDistance(dstTravelled, end));        
    }
}
