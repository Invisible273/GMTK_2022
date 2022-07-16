using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const float ROLL_THRESHOLD = 5f;

    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxRollSpeed;
    [SerializeField] private float rollSpeedDecay;

    private PlayerController playerController;
    private Rigidbody2D rb;
    private Vector2 walkVector;
    private float currentRollSpeed;
    private Vector3 currentRollDir;

    private State state;
    private enum State
    {
        Normal,
        Rolling,
    }

    private void Start() {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        playerController.onMovementInput += OnMovementInputRecieved;
        playerController.onRollInput += OnRollInputRecieved;

        walkVector = Vector2.zero;
        state = State.Normal;
    }

    private void OnMovementInputRecieved(Vector2 movementDir) {
        walkVector = movementDir * movementSpeed;
    }

    private void OnRollInputRecieved(Vector2 movementDir) {
        state = State.Rolling;
        currentRollSpeed = maxRollSpeed;
        currentRollDir = new Vector3(movementDir.x, movementDir.y, 0);
    }

    private void FixedUpdate() {
        switch(state) {
            case State.Normal:
                HandleMovement();
                break;
            case State.Rolling:
                HandleRolling();
                break;
        }
    }

    private void HandleMovement() {
        rb.velocity = walkVector;
    }

    private void HandleRolling() {
        transform.position += currentRollDir * currentRollSpeed * Time.deltaTime;
        currentRollSpeed -= currentRollSpeed * rollSpeedDecay * Time.deltaTime;
        if(currentRollSpeed < ROLL_THRESHOLD)
            state = State.Normal;
    }
}