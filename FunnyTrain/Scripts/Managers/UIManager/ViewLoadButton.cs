using UnityEngine;

public class ViewLoadButton : MonoBehaviour
{
    [SerializeField] GameObject loadButton;

    public void EnableButton()
    {
        loadButton.SetActive(true);
    }

    public void DisableButton()
    {
        loadButton.SetActive(false);
    }

}
