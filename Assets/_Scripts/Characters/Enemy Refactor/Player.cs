using System;
using UnityEngine;

namespace GMTK2022
{
    [RequireComponent(typeof(PlayerController))]
    public class Player : Character
    {
        private const float ROLL_THRESHOLD = 5f;

        [SerializeField] private LayerMask collisionLayerMask;
        [SerializeField] private float maxRollSpeed;
        [SerializeField] private float rollSpeedDecay;
        [SerializeField] GameObject deadPlayer;
        
        
        private PlayerController playerController;
        private float currentRollSpeed;
        private Vector3 currentRollDir;
        public Action<Vector2> OnRoll;
        public Action<Vector2> OnRollEnd;

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

            playerController.onMovementInput += OnDirectionRecieved;
            playerController.onRollInput += OnRollInputRecieved;
            playerController.onMousePositionUpdate += OnTargetUpdate; 
            state = State.Normal;
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

        public void Death()
        {
            if(deadPlayer)
            {
            Instantiate(deadPlayer, transform.position, Quaternion.identity);
            }
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
