using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class Enemy : Character
{
    private Shooter shooter;
    private EnemyController enemyController;

    protected override void Awake()
    {
        base.Awake();
        shooter = GetComponentInChildren<Shooter>();
        enemyController = GetComponent<EnemyController>();

        enemyController.onMove2Target += OnDirectionRecieved;
        enemyController.onShoot2Target += OnShootAtTarget;
        enemyController.onRotate2Target += OnTargetUpdate;
    }

    private void OnShootAtTarget(Vector2 targetPos2D)
    {
        Vector3 targetPos = new Vector3(targetPos2D.x, targetPos2D.y, transform.position.z);
        shooter.Shoot(targetPos);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
}
