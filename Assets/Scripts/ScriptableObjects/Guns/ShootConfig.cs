using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShootConfig", menuName = "Guns/ShootConfig", order = 2)]
    public class ShootConfig : ScriptableObject
    {
        public LayerMask hitMask;
        public Vector3 spread = new Vector3(0.1f, 0.1f, 0.1f);
        public float fireRate = 0.25f;
        public float reloadTime = 2f;
    }
}