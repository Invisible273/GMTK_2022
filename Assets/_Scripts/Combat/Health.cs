using UnityEngine;
using System;

namespace GMTK2022
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
        public event Action onDeath;
        float health;
        private void Awake() {
            health = maxHealth;
        }

        public void GetDamaged(float amount) {
            if(health - amount > 0) {
                health -= amount;
            } else if(health - amount <= 0) {
                health = 0;
                Die();
            }
        }

        private void Die() {
            onDeath?.Invoke();
        }
        public float GetMaxHealth() {
            return maxHealth;
        }
        public float GetCurrentHealth() {
            return health;
        }

    }
}
