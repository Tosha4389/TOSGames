using UnityEngine;
using UnityEngine.EventSystems;

public class FreeCameraController : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] float speed = 50f;
    Camera cam;
    float deltaRotation;
    float angleY;
    float angleX;

    private void Start()
    {
        cam = Camera.main;
        deltaRotation = Time.deltaTime * speed;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        angleY += eventData.delta.x * deltaRotation;
        angleX += eventData.delta.y * deltaRotation;
        cam.transform.rotation = Quaternion.Euler(-Mathf.Clamp(angleX, -30f, 20f), angleY, cam.transform.rotation.z);        
    }
}
