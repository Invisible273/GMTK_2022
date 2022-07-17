using System.Collections;
using UnityEngine;

namespace GMTK2022
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerController _playerController;

        public AnimationClip attackClip;

        private Vector2 _lastMovementInput;
        private Vector3 _lastMouseInput;
        private bool _isRolling;
        private bool _isAttacking;

        private void Awake() {
            _animator = GetComponent<Animator>();
            if(!_player || !_playerController) Debug.LogError("Player controllers not set!");
            _lastMovementInput = Vector2.zero;
            _lastMouseInput = Vector2.zero;
            _isRolling = false;
        }

        private void OnEnable() {
            _playerController.onMovementInput += UpdateMovementVector;
            _playerController.onMousePositionUpdate += UpdateMouseVector;
            _playerController.OnAttackInput += AttackStarted;
            _player.OnRoll += RollingStarted;
            _player.OnRollEnd += RollingComplete;
        }

        private void OnDisable() {
            _playerController.onMovementInput -= UpdateMovementVector;
            _playerController.onMousePositionUpdate -= UpdateMouseVector;
            _playerController.OnAttackInput -= AttackStarted;
            _player.OnRoll -= RollingStarted;
            _player.OnRollEnd -= RollingComplete;
        }

        private void Update() {
            var state = GetState();

            if(state == _currentState) return;
            _animator.CrossFade(state, 0, 0);
            _currentState = state;
        }

        private void UpdateMovementVector(Vector2 direction) {
            if(direction == Vector2.zero) {
                _animator.SetFloat("idleX", _lastMovementInput.x);
                _animator.SetFloat("idleY", _lastMovementInput.y);
                _lastMovementInput = direction;

                return;
            }
            _lastMovementInput = direction;

            _animator.SetFloat("movementX", direction.x);
            _animator.SetFloat("movementY", direction.y);
        }

        private void UpdateMouseVector(Vector3 position) {
            _lastMouseInput = position;
        }

        private void RollingStarted(Vector2 direction) {
            _isRolling = true;
            _animator.SetFloat("rollX", direction.x);
            _animator.SetFloat("rollY", direction.y);
        }

        private void RollingComplete(Vector2 direction) {
            _isRolling = false;
        }

        private void AttackStarted() {
            Vector3 mouseVector = _lastMouseInput - transform.position;
            mouseVector.Normalize();
            _animator.SetFloat("attackX", mouseVector.x);
            _animator.SetFloat("attackY", mouseVector.y);

            StartCoroutine(WaitForAttackAnimation(attackClip.length));
        }

        private IEnumerator WaitForAttackAnimation(float duration) {
            _isAttacking = true;
            yield return new WaitForSeconds(duration);
            _isAttacking = false;
        }
                
        private int GetState() {
            //Highest priority checked first
            if(_isAttacking) return Attack;
            if(_isRolling) return Roll;
            return _lastMovementInput == Vector2.zero ? Idle : Walk;
        }

        #region Cached Properties

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("Idle Blend Tree");
        private static readonly int Walk = Animator.StringToHash("Walk Blend Tree");
        private static readonly int Roll = Animator.StringToHash("Roll Blend Tree");
        private static readonly int Attack = Animator.StringToHash("Attack Blend Tree");

        #endregion
    }
}
