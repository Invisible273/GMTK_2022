using System.Collections;
using UnityEngine;

namespace GMTK2022
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 750f;
        [SerializeField] float lifetime = 5f;
        [SerializeField] Characters target;
        [SerializeField] float damage = 10f;
        bool isDeflected = false;
        Vector2 direction;
        private Rigidbody2D rb;
        Vector3 lastVelocity;
        bool started = false;

        public int value; // Goes from 1 to 6

        // Start is called before the first frame update
        private void OnEnable() {
            rb = GetComponent<Rigidbody2D>();

            // For now the value of the bullet will be random, we'll decide if we want it to depend from other things
            value = Random.Range(1, 6);

            StartCoroutine(WaitAndDisable());

        }

        // Update is called once per frame
        void Update() {
            lastVelocity = rb.velocity;

            MoveTowardsTarget();
        }

        public void SetDirection(Vector3 dir) {
            direction = dir.normalized;
        }

        void MoveTowardsTarget() {
            // if(direction != null && !started) {
            //     rb.AddForce(new Vector2(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime));
            //     started = true;
                GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;
            // }
        }

        IEnumerator WaitAndDisable() {
            yield return new WaitForSeconds(lifetime);
            gameObject.SetActive(false);

        }
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.tag == target.ToString()) {
                Health tHealth = other.GetComponent<Health>();
                if(tHealth) {
                    other.GetComponent<Health>().GetDamaged(damage);
                    gameObject.SetActive(false);
                }
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
                Vector3 curVelocity = GetComponent<Rigidbody2D>().velocity;
                SetDirection(-curVelocity);
                target = Characters.Enemy;
                isDeflected = true;
            }
            
        }

    }
}
