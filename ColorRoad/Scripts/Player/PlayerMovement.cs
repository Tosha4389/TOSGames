using PathCreation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField] PathCreator pathNew;

    Rigidbody rigidB;
    UIManager uiManager;
    EndOfPathInstruction end;
    float dstTravelled;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();         
    }

    private void Start()
    {
        dstTravelled = 0f;
        uiManager = UIManager.S;
    }

    public void Move(float speed)
    {
        if(speed > 0) {

            dstTravelled += Time.deltaTime * speed;     
            
            rigidB.MovePosition(pathNew.path.GetPointAtDistance(dstTravelled, end));
            rigidB.MoveRotation(pathNew.path.GetRotationAtDistance(dstTravelled, end));

            uiManager.UpdateCountDistance(dstTravelled / pathNew.path.length);
        }
        else { }
    }
}
