using UnityEngine;

internal interface IMovement
{
    void Movement(Vector3 direction);

    void Jump(Vector3 direction);
}
