using System.Collections;
using UnityEngine;

namespace GMTK2022
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 25f;
        [SerializeField] float lifetime = 1f;
        [SerializeField] Characters target;
        [SerializeField] float damage = 10f;
        bool isDeflected = false;
        Vector2 direction;
        private Rigidbody2D rb;

        public int value; // Goes from 1 to 6

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        private void OnEnable() {
            target = Characters.Player;
            // For now the value of the bullet will be random, we'll decide if we want it to depend from other things
            value = Random.Range(1, 6);

            StartCoroutine(WaitAndDisable());
        }

        private void FixedUpdate() {
            rb.velocity = direction * speed;
        }

        public void SetDirection(Vector3 dir) {
            direction = dir.normalized;
        }

        IEnumerator WaitAndDisable() {
            yield return new WaitForSeconds(lifetime);
            gameObject.SetActive(false);

        }
        private void OnTriggerEnter2D(Collider2D other) {
            if(target == Characters.Player && other.TryGetComponent<PlayerHealth>(out var playerHealth)) {
                if(playerHealth.Invincible) return;
                playerHealth.GetDamaged(damage);
                gameObject.SetActive(false);
            }
            else if(target == Characters.Enemy && other.TryGetComponent<Health>(out var enemyHealth)) {
                enemyHealth.GetDamaged(damage);
                gameObject.SetActive(false);
            }
        }

        // private void OnCollisionEnter2D(Collision2D collision) {
        //     //print(collision.gameObject.layer);
        //     var speed = lastVelocity.magnitude;
        //     var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        //     rb.velocity = direction * Mathf.Max(speed, 0f);
        // }

        public void GetDeflected()
        {
            if(!isDeflected)
            {
                Vector3 curVelocity = rb.velocity;
                SetDirection(-curVelocity);
                target = Characters.Enemy;
                isDeflected = true;
            }
            
        }

    }
}
