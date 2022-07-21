using UnityEngine;

namespace GMTK2022
{
    [RequireComponent(typeof(EnemyController))]
    public class Enemy : Character
    {
        [SerializeField]
        private int enemyValue = 1;

        private Shooter shooter;
        private EnemyController enemyController;
        Health eHealth = null;
        

        protected override void Awake() {
            base.Awake();
            shooter = GetComponentInChildren<Shooter>();
            eHealth = GetComponent<Health>();
            if (eHealth)
            {
                eHealth.onDeath += Die;
            }
            enemyController = GetComponent<EnemyController>();

            enemyController.onMove2Target += OnDirectionRecieved;
            enemyController.onShoot2Target += OnShootAtTarget;
            enemyController.onRotate2Target += OnTargetUpdate;
        }

        private void OnShootAtTarget(Vector2 targetPos2D) {
            Vector3 targetPos = new Vector3(targetPos2D.x, targetPos2D.y, transform.position.z);
            shooter.Shoot(targetPos);
        }

        protected override void OnTargetUpdate(Vector3 targetTransform) {
            weaponRotator.Rotate2TargetSnap(targetTransform);
        }

        private void FixedUpdate() {
            HandleMovement();
        }

        public void Die()
        {
            eHealth.onDeath -= Die;
            Destroy(gameObject);
        }

        private void OnDestroy() {
            GameplayManager.instance.AddScore(enemyValue);
        }
    }
}
