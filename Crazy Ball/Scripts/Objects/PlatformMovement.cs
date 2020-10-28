using UnityEngine;
using DG.Tweening;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] bool moveY = false;
    [SerializeField] float height;

    [SerializeField] bool moveX = false;
    [SerializeField] float width;

    float startX;
    float finishX;
    float startY;
    float finishY;

    void Start()
    {
        startY = transform.position.y;
        finishY = startY - height;
        startX = transform.position.x - width;
        finishX = transform.position.x + width;

        if(moveY)
            transform.DOMoveY(finishY, duration);
        if(moveX)
            transform.DOMoveX(startX, duration);
    }
    
    void FixedUpdate()
    {
        if(moveX)
            MovePlatformX();

        if(moveY)
            MovePlatformY();
    }

    void MovePlatformY()
    {
        if(transform.position.y == finishY)
            transform.DOMoveY(startY, duration * Time.fixedDeltaTime * 50);

        if(transform.position.y == startY)
            transform.DOMoveY(finishY, duration * Time.fixedDeltaTime * 50);
    }

    void MovePlatformX()
    {
        if(transform.position.x == finishX)
            transform.DOMoveX(startX, duration * Time.fixedDeltaTime * 50);

        if(transform.position.x == startX)
            transform.DOMoveX(finishX, duration * Time.fixedDeltaTime * 50);
    }


}
