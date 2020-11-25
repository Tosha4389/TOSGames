using UnityEngine;

public class SpinnerMovement : MonoBehaviour, IMovement
{
    [SerializeField] bool leftRotate;
    [SerializeField] bool rightRotate;
    Transform transformBody;

    private void Awake()
    {
        transformBody = GetComponent<Transform>();

    }

    public void Move(float speed)
    {
        if(rightRotate && !leftRotate)            
            transform.Rotate(new Vector3(0f, Time.deltaTime * 5 * speed, 0f));
        if(!rightRotate && leftRotate)            
            transform.Rotate(new Vector3(0f, -Time.deltaTime * 5 * speed, 0f));
    }
}
