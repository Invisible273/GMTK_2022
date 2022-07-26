using UnityEngine;

namespace GMTK2022
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;

        private Vector2 walkVector;
        private Rigidbody2D rb;

        protected WeaponRotator weaponRotator;

        protected virtual void Awake() {
            rb = GetComponent<Rigidbody2D>();
            weaponRotator = GetComponentInChildren<WeaponRotator>();

            walkVector = Vector2.zero;
        }

        protected virtual void OnMoveDirectionRecieved(Vector2 movementDir) {
            walkVector = movementDir * movementSpeed;
        }

        protected virtual void OnTargetUpdate(Vector3 targetTransform) {
            
           //weaponRotator.Rotate2Target(targetTransform);
        }

        protected void HandleMovement() {
            rb.velocity = walkVector;
        }
    }
}
