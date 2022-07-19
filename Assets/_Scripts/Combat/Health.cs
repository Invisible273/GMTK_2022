using UnityEngine;

namespace GMTK2022
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
        float health;
        private void Awake() {
            health = maxHealth;
        }

        public void GetDamaged(float amount) {
            if(health - amount > 0) {
                health -= amount;
            } else if(health - amount <= 0) {
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }
        public float GetMaxHealth() {
            return maxHealth;
        }
        public float GetCurrentHealth() {
            return health;
        }

    }
}
