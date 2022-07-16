using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 previousMovementVector;

    private void Start()
    {
        previousMovementVector = Vector2.zero;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 movementVector = new Vector2(horizontal, vertical);
        
        if (movementVector != previousMovementVector)
        {
            MovementInputRecieved(movementVector);
            previousMovementVector = movementVector;
        }
    }

    public delegate void MovementInput (Vector2 movementDir);
    public event MovementInput onMovementInput;
    public void MovementInputRecieved(Vector2 movDir)
    {
        onMovementInput?.Invoke(movDir);
    }
}
