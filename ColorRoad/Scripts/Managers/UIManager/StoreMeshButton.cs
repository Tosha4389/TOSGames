using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class StoreMeshButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] PlayerMesh currentMesh;
    [SerializeField] int price;
    [SerializeField] Image currentImage;
    [SerializeField] TextMeshProUGUI textPrice;
    [SerializeField] Image disactive;

    GameManager gameManager;
    UIManager uiManager;
    Image imageIcon;
    bool isBay;

    private void Awake()
    {
        imageIcon = GetComponent<Image>();
    }

    private void Start()
    {
        gameManager = GameManager.S;
        uiManager = UIManager.S;

        if(PlayerPrefs.HasKey(currentMesh.ToString())) {
            isBay = true;
            price = 0;
        }

        if(price > 0) {
            textPrice.text = price.ToString();
        }
        else { 
            textPrice.text = "";
            disactive.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isBay) {
            BayMesh();
        } else ActiveMesh();
    }

    void BayMesh()
    {
        if(gameManager.coins >= price) {
            gameManager.DecreaseCoins(price);
            textPrice.text = "";
            price = 0;
            disactive.enabled = false;
            gameManager.playerPrefs.SavePlayerPrefs(currentMesh.ToString(), (int)currentMesh);
            ActiveMesh();
        }
    }

    void ActiveMesh()
    {
        gameManager.playerPrefs.SavePlayerPrefs("PlayerMesh", (int)currentMesh);
        //currentImage.sprite = uiManager.meshIcon[(int)currentMesh];
        currentImage.sprite = imageIcon.sprite;
        gameManager.playerMesh.ChangeMesh((int)currentMesh);
    }
}
