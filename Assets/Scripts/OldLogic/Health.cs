using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        private int _health;
        public int maxHealth;

        void Start()
        {
            _health = maxHealth;
        }

        public int CurrentHealth()
        {
            return _health;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public bool IsDead()
        {
            return _health <= 0;
        }

        public bool WillBeDead(int damage)
        {
            int predictedHealth = _health - damage;
            return predictedHealth <= 0;
        }
    }
}