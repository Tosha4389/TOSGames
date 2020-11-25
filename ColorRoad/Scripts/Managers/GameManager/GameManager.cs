using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    internal static GameManager S;

    [SerializeField] bool vSync;
    [SerializeField] int frameRate;
    [SerializeField] bool loadCurrentLevel;

    internal int coins;
    internal PlayerPrefsController playerPrefs;
    internal ChangePlayerMesh playerMesh;

    UIManager uiManager;

    private void Awake()
    {
        if(S == null)
            S = this;
        else {
            Debug.Log(gameObject.name + ".Awake(): Ошибка. Создан второй " + gameObject.name);
            Destroy(this.gameObject);
        }

        if(vSync && Application.platform == RuntimePlatform.WindowsEditor)
            Application.targetFrameRate = frameRate;
        else Application.targetFrameRate = 2000;
            
        playerPrefs = GetComponent<PlayerPrefsController>();
        playerMesh = GetComponent<ChangePlayerMesh>();

        if(loadCurrentLevel)
            LoadCurrentLevel();
    }

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        uiManager = UIManager.S;        
        InitLevel();
    }

    void InitLevel()
    {
        DOTween.KillAll();
        playerMesh.ChangeMesh(playerPrefs.LoadPlayerPrefs("PlayerMesh"));
        coins = playerPrefs.LoadPlayerPrefs("PlayerCoins");
        uiManager.ViewCoinsScore(coins);
        uiManager.UpdateCountDistance(0f);
    }

    void LoadCurrentLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 && PlayerPrefs.HasKey("PlayerLevel")) {
            SceneManager.LoadScene(playerPrefs.LoadPlayerPrefs("PlayerLevel"));
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameWin()
    {
        playerPrefs.SavePlayerPrefs("PlayerCoins", coins);
        playerPrefs.SavePlayerPrefs("PlayerLevel", SceneManager.GetActiveScene().buildIndex + 1);

        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            int scene = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(scene);
        }

        if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
    }

    public void IncreaseCoins(int value)
    {
        coins += value;
        uiManager.ViewCoinsScore(coins);
    }

    public void DecreaseCoins(int value)
    {
        int diff = coins - value;
        if(diff >= 0)
            coins -= value;
        else { }

        playerPrefs.SavePlayerPrefs("PlayerCoins", coins);
        uiManager.ViewCoinsScore(coins);
    }
}
