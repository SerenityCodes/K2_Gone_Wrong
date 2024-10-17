using System.Collections;
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
                StartCoroutine(Die());
            }
        }

        IEnumerator Die()
        {
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("Stop Walking");
            Destroy(gameObject);
        }
    }
}