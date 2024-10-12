using UnityEngine;

namespace DefaultNamespace
{
    public class Target : MonoBehaviour
    {
        public float health = 50f;
        public Animator animator;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            animator.SetTrigger("Die");
            Destroy(gameObject);
        }
    }
}