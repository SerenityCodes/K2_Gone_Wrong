using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CrateInteraction : MonoBehaviour
    {
        private Interactable _interactable;
        
        private void Start()
        {
            _interactable = GetComponent<Interactable>();
        }

        public void ChangeMessage()
        {
            if (!GameManager.Instance.IsPowerActive())
            {
                _interactable.message = "Power is disabled";
                _interactable.showKeyBind = false;
            }
            else
            {
                _interactable.message = "Pick up CO2 Crate";
                _interactable.showKeyBind = true;
            }
        }
    }
}