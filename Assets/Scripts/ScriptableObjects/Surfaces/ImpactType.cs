using UnityEngine;

namespace DefaultNamespace.ScriptableObjects.Surfaces
{
    [CreateAssetMenu(menuName = "Impact System/Impact Type", fileName = "ImpactType")]
    public class ImpactType : ScriptableObject
    {
        public new string name;
    }
}