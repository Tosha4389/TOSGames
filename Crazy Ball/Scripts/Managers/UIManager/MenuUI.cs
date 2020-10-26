using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour, IMenuUI
{
    [SerializeField] GameObject menu;
    bool onMenu = false;

    public void LoadSceneMenu(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OnOffMenu()
    {
        if(!onMenu) {
            Time.timeScale = 0f;
            menu.SetActive(true);
            onMenu = true;            
        } else if(onMenu) {
            Time.timeScale = 1f;
            menu.SetActive(false);            
            onMenu = false;
        }
    }
}
