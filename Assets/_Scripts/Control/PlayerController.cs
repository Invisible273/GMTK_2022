using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInputActions playerInput;
    private Vector2 previousMovementVector;
    private Vector3 mouseWorldPos;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Roll.performed += Roll;
        playerInput.Player.Attack.performed += Attack;
    }
    
    private void Start()
    {
        previousMovementVector = Vector2.zero;
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Update()
    {
        Vector2 movementVector = playerInput.Player.Move.ReadValue<Vector2>();

        if (movementVector != previousMovementVector)
        {
            MovementInputRecieved(movementVector);
            previousMovementVector = movementVector;
        }

        //Processing mouse movement
        Vector3 mousePos = Mouse.current.position.ReadValue();   
        mousePos.z = Camera.main.nearClipPlane;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        MousePositionUpdate(mouseWorldPos);  
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Vector2 movementVector = playerInput.Player.Move.ReadValue<Vector2>();
            if (movementVector == Vector2.zero)
                return;
            if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.y))
            {
                Vector2 rollDir = Mathf.Sign(movementVector.x) * Vector2.right;
                RollInputRecieved(rollDir);
            }
            else
            {
                Vector2 rollDir = Mathf.Sign(movementVector.y) * Vector2.up;
                RollInputRecieved(rollDir);
            }
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            
        }
    }

    public delegate void MovementInput (Vector2 movementDir);
    public event MovementInput onMovementInput;
    public void MovementInputRecieved(Vector2 movDir)
    {
        onMovementInput?.Invoke(movDir);
    }

    public event MovementInput onRollInput;
    public void RollInputRecieved(Vector2 movDir)
    {
        onRollInput?.Invoke(movDir);
    }

    public delegate void MousePosition (Vector3 mousePos);
    public event MousePosition onMousePositionUpdate;
    public void MousePositionUpdate(Vector3 mousePos)
    {
        onMousePositionUpdate?.Invoke(mousePos);
    }
}
