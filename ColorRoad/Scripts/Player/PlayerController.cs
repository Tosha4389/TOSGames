using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : ObjectController
{
    [SerializeField] AudioEvents playerDestroy;
    [SerializeField] AudioEvents gameWin;

    IMovement movement;
    InputManager inputManager;
    GameManager gameManager;
    ParticleSystem particleSys;
    MeshRenderer mesh;
    bool gameOver;

    private void Awake()
    {
        movement = GetComponent<IMovement>();
        particleSys = GetComponent<ParticleSystem>();
        mesh = gameObject.GetComponent<MeshRenderer>();

        if(playerDestroy == null)
            playerDestroy = new AudioEvents();

        if(gameWin == null)
            gameWin = new AudioEvents();
    }

    private void Start()
    {
        inputManager = InputManager.S;
        gameManager = GameManager.S;
    }

    private void Update()
    {
        Move(duration);
    }

    public override void Move(float speed)
    {
        if(inputManager.moveOn && !gameOver)
            movement.Move(speed);
        else movement.Move(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Enemy") && !gameOver) {
            StartCoroutine(DestroyPlayer());
        }

        if(other.transform.CompareTag("Finish")) {
            gameWin.Invoke();
            StartCoroutine(DelayGameWin());
        }
    }

    IEnumerator DestroyPlayer()
    {
        playerDestroy.Invoke();
        gameOver = true;
        particleSys.Play();
        mesh.enabled = false;
        yield return new WaitForSeconds(particleSys.main.duration);
        gameManager.GameOver();
    }

    IEnumerator DelayGameWin()
    {
        gameOver = true;
        gameWin.Invoke();
        yield return new WaitForSeconds(1f);
        gameManager.GameWin();
    }
}
