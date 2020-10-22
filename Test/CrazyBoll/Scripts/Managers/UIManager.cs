using UnityEngine;

public class UIManager : MonoBehaviour
{
    static internal UIManager S;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("UIManager.Awake() - создан второй UIManager");
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
