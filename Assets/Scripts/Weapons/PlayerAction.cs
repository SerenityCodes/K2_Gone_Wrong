using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PlayerAction : MonoBehaviour
    {
        private PlayerGunSelector _gunSelector;

        private void Start()
        {
            _gunSelector = GetComponent<PlayerGunSelector>();
        }

        private void Update()
        {
            if (Mouse.current.leftButton.isPressed && _gunSelector.activeGun != null)
            {
                _gunSelector.activeGun.Shoot();
            }
        }
    }
}