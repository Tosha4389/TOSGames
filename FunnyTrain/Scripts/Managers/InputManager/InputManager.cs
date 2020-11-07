using UnityEngine;

public class InputManager : MonoBehaviour
{
    static internal InputManager S;
    InputJoysticks inputJoystick;

    private void Awake()
    {
        if(S == null) {
            S = this;
        } else {
            Debug.LogError("InputManager.Awake() - создан второй InputManager");
            Destroy(gameObject);
        }

        inputJoystick = GetComponent<InputJoysticks>();
    }

    public float InputJoystickTrain()
    {
        float inputJoy = inputJoystick.InputJoystickTrain();
        return inputJoy;
    }

    public float InputJoystickCamera()
    {
        float inputJoy = inputJoystick.InputJoystickCamera();
        return inputJoy;
    }
}
