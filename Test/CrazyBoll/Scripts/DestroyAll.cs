using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    [SerializeField] Transform player;
    Transform trans;

    void Awake()
    {
        trans = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        if(player)
            trans.position = player.position;
    }
}
