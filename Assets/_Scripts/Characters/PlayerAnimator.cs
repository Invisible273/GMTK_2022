using UnityEngine;

namespace GMTK2022
{
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerController _playerController;

        private Vector2 _lastMovementInput;
        private bool _isRolling;

        private void Awake() {
            _animator = GetComponent<Animator>();
            if(!_player || !_playerController) Debug.LogError("Player controllers not set!");
            _lastMovementInput = Vector2.zero;
            _isRolling = false;
        }

        private void OnEnable() {
            _playerController.onMovementInput += UpdateMovementVector;
            _player.OnRoll += RollingStarted;
            _player.OnRollEnd += RollingComplete;
        }

        private void OnDisable() {
            _playerController.onMovementInput -= UpdateMovementVector;
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

        private void RollingStarted(Vector2 direction) {
            _isRolling = true;
            _animator.SetFloat("rollX", direction.x);
            _animator.SetFloat("rollY", direction.y);
        }

        private void RollingComplete(Vector2 direction) {
            _isRolling = false;
        }

        private int GetState() {
            if(_isRolling) return Roll;
            return _lastMovementInput == Vector2.zero ? Idle : Walk;
        }

        #region Cached Properties

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("Idle Blend Tree");
        private static readonly int Walk = Animator.StringToHash("Walking Blend Tree");
        private static readonly int Roll = Animator.StringToHash("Roll Blend Tree");

        #endregion
    }
}
