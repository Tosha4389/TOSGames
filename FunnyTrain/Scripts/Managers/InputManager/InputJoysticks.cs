using UnityEngine;

public class InputJoysticks : MonoBehaviour
{
    [SerializeField] FixedJoystick joystickTrain;
    [SerializeField] FixedJoystick joystickCamera;

    public float InputJoystickTrain()
    {
        float inputJoy = joystickTrain.Vertical;
        return inputJoy;
    }

    public float InputJoystickCamera()
    {
        float inputJoy = joystickCamera.Vertical;
        return inputJoy;
    }
}
