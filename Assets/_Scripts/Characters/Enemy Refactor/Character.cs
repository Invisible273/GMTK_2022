using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Vector2 walkVector;
    private Rigidbody2D rb;

    protected WeaponRotator weaponRotator;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weaponRotator = GetComponentInChildren<WeaponRotator>();

        walkVector = Vector2.zero;
    }

    protected void OnDirectionRecieved(Vector2 movementDir)
    {
        walkVector = movementDir * movementSpeed;
    }

    protected virtual void OnTargetUpdate(Vector3 targetTransform)
    {
        weaponRotator.Rotate2Target(targetTransform);
    }

    protected void HandleMovement()
    {
        rb.velocity = walkVector;
    }
}
