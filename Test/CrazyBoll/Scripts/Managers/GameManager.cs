using UnityEngine;

public class GameManager : MonoBehaviour
{
    static internal GameManager S;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("GameManager.Awake() - создан второй GameManager");
            Destroy(gameObject);
        }        
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
