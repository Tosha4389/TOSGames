using UnityEngine;
using DG.Tweening;

public class SpeareMovement : MonoBehaviour, IMovement
{
    [SerializeField] bool moveY;
    [SerializeField] float finishY;
    [SerializeField] bool moveX;
    [SerializeField] float finishX;

    private void Start()
    {
        
    }

    public void Move(float speed)
    {
        if(moveY)
            transform.DOMoveY(finishY, speed).SetLoops(-1, LoopType.Yoyo);

        if(moveX)
            transform.DOMoveX(finishX, speed).SetLoops(-1, LoopType.Yoyo);
    }
}
