using UnityEngine;

public class DestroyGO : MonoBehaviour, IDestroyGO
{
    public void DestroyObjects()
    {
        gameObject.SetActive(false);
    }
}
