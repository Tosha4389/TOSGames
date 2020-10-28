using UnityEngine;
using DG.Tweening;

public class ChangeColorScript : MonoBehaviour
{
    [SerializeField] float duration;

    Renderer rend;
    Color color;

    void Start()
    {
        rend = GetComponent<Renderer>();
        color = new Color(Random.value, Random.value, Random.value, 1f);
        rend.material.DOColor(color, duration);
    }

    
    void FixedUpdate()
    {
        if(rend.material.color == color) {
            color = new Color(Random.value, Random.value, Random.value, 1f);
            rend.material.DOColor(color, duration);
        }
    }
}
