using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    internal static UIManager S;

    [SerializeField] TextMeshProUGUI coinsScore;
    [SerializeField] Slider slider;
    [SerializeField] GameObject storeMenu;
    [SerializeField] Image currentMeshImage;
    [SerializeField] internal List<Sprite> meshIcon;

    private void Awake()
    {
        if(S == null)
            S = this;
        else {
            Debug.Log(gameObject.name + ".Awake(): Ошибка. Создан второй " + gameObject.name);
            Destroy(this.gameObject);
        }        
    }

    private void Start()
    {
        InitUI();
    }

    void InitUI()
    {
        currentMeshImage.sprite = meshIcon[PlayerPrefs.GetInt("PlayerMesh", 0)];
    }

    public void ViewCoinsScore(int value)
    {
        coinsScore.text = value.ToString();
    }

    public void ViewStoreMenu()
    {
        if(!storeMenu.activeInHierarchy)
            storeMenu.SetActive(true);
        else storeMenu.SetActive(false);
    }

    public void UpdateCountDistance(float scale)
    {        
        slider.value = scale;
    }
}
