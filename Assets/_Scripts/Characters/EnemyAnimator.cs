using UnityEngine;
using System.Collections;
namespace GMTK2022
{
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private EnemyController _enemyController;
        public AnimationClip deathClip;

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

        public IEnumerator PlayDeathAnimationAndDestroy()
        {
            _animator.Play("Dead", 0);
            float duration = deathClip.length;
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
    }
}
