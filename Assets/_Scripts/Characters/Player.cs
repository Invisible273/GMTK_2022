using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    private PlayerController playerController;
    private Rigidbody2D rb;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        playerController.onMovementInput += OnMovementInputRecieved;
    }

    private void OnMovementInputRecieved(Vector2 movementDir)
    {
        rb.velocity = movementDir * movementSpeed;
    }
}
