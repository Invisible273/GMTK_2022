using UnityEngine;

namespace GMTK2022
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private EnemyController _enemyController;

        private void Awake() {
            _animator = GetComponent<Animator>();
            _enemyController = GetComponent<EnemyController>();
        }

        private void OnEnable() {
            _enemyController.onRotate2Target += UpdateAnimationDirection;
            _enemyController.onMove2Target += UpdateAnimationDirection2D;
        }

        private void OnDisable() {
            _enemyController.onRotate2Target -= UpdateAnimationDirection;
            _enemyController.onMove2Target -= UpdateAnimationDirection2D;
        }

        private void UpdateAnimationDirection(Vector3 direction) {
            UpdateAnimationDirection2D(direction);
        }

        private void UpdateAnimationDirection2D(Vector2 direction) {
            _animator.SetFloat("lookX", direction.x);
            _animator.SetFloat("lookY", direction.y);
        }
    }
}
