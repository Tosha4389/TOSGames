using UnityEngine;

public class PlayerDestroy : MonoBehaviour, IDestroyGO
{
    public void DestroyObjects()
    {
        Destroy(gameObject); 
    }
}
