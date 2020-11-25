using UnityEngine;

public abstract class ObjectController : MonoBehaviour
{
    [SerializeField] protected float duration;

    public abstract void Move(float speed);
}
