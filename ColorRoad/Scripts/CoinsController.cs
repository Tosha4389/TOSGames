using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsController : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] AudioEvents enterPlayer;
    [SerializeField] int value;
    [SerializeField] float speedRotate;

    Transform parent;

    private void Awake()
    {
        if(enterPlayer == null)
            enterPlayer = new AudioEvents();
    }

    private void Start()
    {
        gameManager = GameManager.S;
        parent = transform.parent;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 30f * Time.deltaTime * speedRotate));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")) {
            gameManager.IncreaseCoins(value);            
            parent.gameObject.SetActive(false);
            enterPlayer.Invoke();
        }
    }
}
