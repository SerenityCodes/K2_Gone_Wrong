using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerDisplayHealth : MonoBehaviour
    {
        public TMP_Text healthText;
        
        private Health _health;

        void Start()
        {
            _health = GetComponent<Health>();
        }

        void Update()
        {
            healthText.text = "Health: " + _health.CurrentHealth();
        }
    }
}