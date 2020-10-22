using UnityEngine;

public class EnemyDestroy : MonoBehaviour, IDestroyGO
{
    public void DestroyObjects()
    {
        gameObject.SetActive(false);
    }
}
