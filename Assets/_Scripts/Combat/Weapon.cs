using System;
using System.Collections;
using UnityEngine;

namespace GMTK2022
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Collider2D _hitBox;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private WeaponRotator _weaponRotator;
        [SerializeField] private AnimationClip _attackClip;
        [SerializeField] private float _attackDuration = 0.3f;
        private bool _isAttacking = false;
        private Vector3 _lastMouseInput = Vector3.zero;

        public event Action<Vector2> OnAttackStart;
        public event Action OnAttackEnd;

        // [Header("Parrying")]
        // [SerializeField] float parryTimer = 0.25f;
        // public bool isParrying = false;
        // [SerializeField] SpriteRenderer spriteRenderer;

        // // Start is called before the first frame update
        // void Start() {

        // }

        // // Update is called once per frame
        // void Update() {
        //     if(Input.GetMouseButtonDown(1)) {
        //         StartCoroutine(ParryCoroutine());
        //     }

        //     if(isParrying) {
        //         spriteRenderer.color = new Color(255, 0, 0, 255);
        //     } else {
        //         spriteRenderer.color = new Color(255, 0, 255, 255);
        //     }
        // }

        // private IEnumerator ParryCoroutine() {
        //     isParrying = true;
        //     gameObject.layer = LayerMask.NameToLayer("Parry");
        //     yield return new WaitForSeconds(parryTimer);
        //     gameObject.layer = LayerMask.NameToLayer("Default");
        //     isParrying = false;
        // }

        private void Awake() {
            if(_attackClip != null) _attackDuration = _attackClip.length;
        }

        private void OnEnable()
        {
            _playerController.OnAttackInput += StartAttack;
            _playerController.onMousePositionUpdate += UpdateMouseVector;
        }

        private void UpdateMouseVector(Vector3 position) {
            _lastMouseInput = position;
        }

        private void StartAttack()
        {
            if(_isAttacking) return;

            Vector3 mouseVector = _lastMouseInput - transform.position;
            mouseVector.Normalize();
            _weaponRotator.Rotate2Target(_lastMouseInput);
            _isAttacking = true;
            _hitBox.enabled = true;
            OnAttackStart?.Invoke(mouseVector);

            StartCoroutine(ResetAfterAttackDuration(_attackDuration));
        }

        private IEnumerator ResetAfterAttackDuration(float duration) {
            yield return new WaitForSeconds(duration);
            _isAttacking = false;
            _hitBox.enabled = false;
            OnAttackEnd?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().Die();
            }
            else if(other.gameObject.CompareTag("Projectile"))
            {                
                other.gameObject.GetComponent<Projectile>().GetDeflected();
            }
            
        }

        private void OnDisable()
        {
            _playerController.OnAttackInput -= StartAttack;
            _playerController.onMousePositionUpdate -= UpdateMouseVector;
        }

    }
}
