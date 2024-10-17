using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [DisallowMultipleComponent]
    public class PlayerGunSelector : MonoBehaviour
    {
        private GunType _gunType;
        public Transform gunParent;
        public List<GunObject> guns;
        public Camera mainCamera;
        
        [Header("Runtime Filled")]
        public GunObject activeGun;

        private void Start()
        {
            var gunObj = guns.Find(gun => gun.type == GunType.AssaultRifle);
            activeGun = gunObj;
            gunObj.Spawn(gunParent, this, mainCamera);
        }
    }
}