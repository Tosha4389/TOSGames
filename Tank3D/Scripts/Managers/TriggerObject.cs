using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.S;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.CompareTag("Finish") && other.CompareTag("ShellPlayer")) {
            gameManager.GameWin();
        }

        if(this.gameObject.CompareTag("GameOver") && other.CompareTag("ShellEnemy")) {
            gameManager.GameOver();
        }
    }
}
