using UnityEngine;

public class SwitchViewCamera : MonoBehaviour
{
    [SerializeField] Camera freeCamera;
    [SerializeField] Camera trainCamera;
    [SerializeField] GameObject uiFreeCamera;
    [SerializeField] GameObject uiTrainCamera;

    public void SwitchCamera()
    {
        if(freeCamera.enabled == true)
            SwitchTrainCamera();
        else SwitchFreeCamera();
    }

    public void SwitchFreeCamera()
    {
        freeCamera.enabled = true;
        uiFreeCamera.SetActive(true);
        trainCamera.enabled = false;
        uiTrainCamera.SetActive(false);
    }

    public void SwitchTrainCamera()
    {
        trainCamera.enabled = true;
        uiTrainCamera.SetActive(true);        
        freeCamera.enabled = false;
        uiFreeCamera.SetActive(false);
    }
}
