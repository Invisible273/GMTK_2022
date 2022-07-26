using System.Collections;
using UnityEngine;
using System;

namespace GMTK2022
{
    public class PlayerAnimator : MonoBehaviour
    {
        //That's a large amount of dependencies. May need refactoring.
        [SerializeField] private Animator _animator;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Health _playerHealth;

        //May need to use Unity's animator statemachine for transitions
        //rather than tracking everything here and transitioning manually.
        private Vector2 _lastMovementInput;
        private bool _isRolling;
        private bool _isAttacking;
        private bool _isDead;

        private void Awake() {
            ResetStates();
        }

        public void ResetStates() {
            _lastMovementInput = Vector2.zero;
            _isRolling = false;
            _isAttacking = false;
            _isDead = false;
        }

        private void OnEnable() {
            _playerController.onMovementInput += UpdateMovementVector;
            _weapon.OnAttackStart += AttackStarted;
            _weapon.OnAttackEnd += AttackComplete;
            _player.OnRoll += RollingStarted;
            _player.OnRollEnd += RollingComplete;
            _playerHealth.onDeath += DeathTriggered;
        }

        private void OnDisable() {
            _playerController.onMovementInput -= UpdateMovementVector;
            _weapon.OnAttackStart -= AttackStarted;
            _weapon.OnAttackEnd -= AttackComplete;
            _player.OnRoll -= RollingStarted;
            _player.OnRollEnd -= RollingComplete;
            _playerHealth.onDeath -= DeathTriggered;
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

        private void AttackStarted(Vector2 direction) 
        {
            _isAttacking = true;
            _animator.SetFloat("attackX", direction.x);
            _animator.SetFloat("attackY", direction.y);
        }

        private void AttackComplete() {
            _isAttacking = false;
        }

        private void DeathTriggered() {
            _isDead = true;
        }
                
        private int GetState() {
            //Highest priority checked first
            if(_isDead) return Dead;
            if(_isAttacking) return Attack;
            if(_isRolling) return Roll;
            return _lastMovementInput == Vector2.zero ? Idle : Walk;
        }

        #region Cached Properties
        //This way of handling animation states was influned by Tarodev

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("Idle Blend Tree");
        private static readonly int Walk = Animator.StringToHash("Walk Blend Tree");
        private static readonly int Roll = Animator.StringToHash("Roll Blend Tree");
        private static readonly int Attack = Animator.StringToHash("Attack Blend Tree");
        private static readonly int Dead = Animator.StringToHash("Death");

        #endregion
    }
}
