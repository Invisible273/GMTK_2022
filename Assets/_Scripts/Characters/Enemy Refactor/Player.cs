using System;
using UnityEngine;

namespace GMTK2022
{
    [RequireComponent(typeof(PlayerController), typeof(Health))]
    public class Player : Character
    {
        private const float ROLL_THRESHOLD = 5f;

        [SerializeField] private LayerMask collisionLayerMask;
        [SerializeField] private float maxRollSpeed;
        [SerializeField] private float rollSpeedDecay;
        [SerializeField] GameObject deadPlayer;
        GameplayManager gManager = null;
        private Health _playerHealth = default;


        private PlayerController playerController;
        private float currentRollSpeed;
        private Vector3 currentRollDir;
        public event Action<Vector2> OnRoll;
        public event Action<Vector2> OnRollEnd;

        private float debugRollTime = 0.0f;

        private State state;
        private enum State
        {
            Normal,
            Rolling,
        }

        protected override void Awake() {
            base.Awake();

            playerController = GetComponent<PlayerController>();
            _playerHealth = GetComponent<Health>();

            state = State.Normal;
            gManager = FindObjectOfType<GameplayManager>();
        }

        private void OnEnable() {
            playerController.onMovementInput += OnDirectionRecieved;
            playerController.onRollInput += OnRollInputRecieved;
            playerController.onMousePositionUpdate += OnTargetUpdate;
            _playerHealth.onDeath += OnDeath;
        }

        private void OnDisable() {
            playerController.onMovementInput -= OnDirectionRecieved;
            playerController.onRollInput -= OnRollInputRecieved;
            playerController.onMousePositionUpdate -= OnTargetUpdate;
            _playerHealth.onDeath -= OnDeath;
        }

        private void OnRollInputRecieved(Vector2 movementDir) {
            state = State.Rolling;
            currentRollSpeed = maxRollSpeed;
            currentRollDir = new Vector3(movementDir.x, movementDir.y, 0);

            OnRoll?.Invoke(movementDir);

            debugRollTime = 0.0f;
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

        private void OnDeath()
        {
            if(deadPlayer)
            {
            Instantiate(deadPlayer, transform.position, Quaternion.identity);
            }
            gManager.GameEnded();
            Destroy(gameObject);
        }

        

        private void HandleRolling() {
            debugRollTime += Time.fixedDeltaTime;

            transform.position += currentRollDir * currentRollSpeed * Time.fixedDeltaTime;
            currentRollSpeed -= currentRollSpeed * rollSpeedDecay * Time.fixedDeltaTime;
            if(currentRollSpeed < ROLL_THRESHOLD) {
                state = State.Normal;
                OnRollEnd?.Invoke(currentRollDir);
                //Debug.Log(debugRollTime);
            }
        }
    }
}
