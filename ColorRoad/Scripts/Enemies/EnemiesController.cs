using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : ObjectController
{
    [SerializeField] bool isSpinner = false;
    IMovement movement;

    private void Awake()
    {
        movement = GetComponent<IMovement>();
    }

    private void Start()
    {        
        Move(duration);
    }

    private void Update()
    {
        if(isSpinner)
            Move(duration);
    }

    public override void Move(float speed)
    {
        movement.Move(speed);
    }
}
