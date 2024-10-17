using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TrailConfig", menuName = "Guns/TrailConfig", order = 1)]
    public class TrailConfig : ScriptableObject
    {
        public Material TrailMaterial;
        public AnimationCurve TrailCurve;
        public float Duration = 0.5f;
        public float minVertexDistance = 0.1f;
        public Gradient Color;

        public float MissDistance = 100f;
        public float SimulationSpeed = 10f;
    }
}