using UnityEngine;

namespace ScriptableObjects.Surfaces
{
    public class SpawnObjectEffect : ScriptableObject
    {
        public GameObject prefab;
        public float probability = 1f;
        public bool RandomRotation;

        [Tooltip("Zero values will lock the rotation on that axis. Values up to 360 are sensible for each X, Y, Z")]
        public Vector3 RandomizedRotationMultiplier = Vector3.zero;
    }
}