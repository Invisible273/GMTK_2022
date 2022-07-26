using UnityEngine;

namespace GMTK2022
{
    public class PlayerAnimator : MonoBehaviour
    {
        #region Dependencies
        //That a decent amount of dependencies. May need refactoring.

        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Health _playerHealth;

        #endregion

        //May need to use Unity's animator statemachine for transitions
        //rather than tracking everything here and transitioning manually.
        private bool _isDead;
        private bool _isAttacking;
        private bool _isRolling;
        private bool _isWalking;

        private void Awake() {
            ResetStates();
        }

        private void OnEnable() {
            _playerHealth.onDeath += DeathTriggered;
            _weapon.OnAttackStart += AttackStarted;
            _weapon.OnAttackEnd += AttackComplete;
            _player.OnRoll += RollingStarted;
            _player.OnRollEnd += RollingComplete;
            _player.OnWalk += UpdateWalkVector;
            _player.OnIdle += IdleStarted;
        }

        private void OnDisable() {
            _playerHealth.onDeath -= DeathTriggered;
            _weapon.OnAttackStart -= AttackStarted;
            _weapon.OnAttackEnd -= AttackComplete;
            _player.OnRoll -= RollingStarted;
            _player.OnRollEnd -= RollingComplete;
            _player.OnWalk -= UpdateWalkVector;
            _player.OnIdle -= IdleStarted;
        }

        private void Update() {
            StateMachineUpdate();
        }

        private void DeathTriggered() {
            _isDead = true;
        }

        private void AttackStarted(Vector2 direction) {
            _isAttacking = true;
            _animator.SetFloat("attackX", direction.x);
            _animator.SetFloat("attackY", direction.y);
        }

        private void AttackComplete() {
            _isAttacking = false;
        }

        private void RollingStarted(Vector2 direction) {
            _isRolling = true;
            _animator.SetFloat("rollX", direction.x);
            _animator.SetFloat("rollY", direction.y);
        }

        private void RollingComplete(Vector2 direction) {
            _isRolling = false;
        }

        private void UpdateWalkVector(Vector2 direction) {
            _isWalking = direction != Vector2.zero;
            _animator.SetFloat("movementX", direction.x);
            _animator.SetFloat("movementY", direction.y);
        }

        private void IdleStarted(Vector2 direction) {
            _animator.SetFloat("idleX", direction.x);
            _animator.SetFloat("idleY", direction.y);
        }

        #region Pseudo State Machine
        //This way of handling animation states was influned by Tarodev

        public void ResetStates() {
            _isRolling = false;
            _isAttacking = false;
            _isDead = false;
            _isWalking = false;
        }

        private int GetState() {
            //Highest priority checked first
            if(_isDead) return Dead;
            if(_isAttacking) return Attack;
            if(_isRolling) return Roll;
            if(_isWalking) return Walk;
            return Idle;
        }

        private void StateMachineUpdate() {
            var state = GetState();

            if(state == _currentState) return;
            _animator.CrossFade(state, 0, 0);
            _currentState = state;
        }


        private int _currentState;

        private static readonly int Dead = Animator.StringToHash("Death");
        private static readonly int Attack = Animator.StringToHash("Attack Blend Tree");
        private static readonly int Roll = Animator.StringToHash("Roll Blend Tree");
        private static readonly int Walk = Animator.StringToHash("Walk Blend Tree");
        private static readonly int Idle = Animator.StringToHash("Idle Blend Tree");

        #endregion
    }
}
