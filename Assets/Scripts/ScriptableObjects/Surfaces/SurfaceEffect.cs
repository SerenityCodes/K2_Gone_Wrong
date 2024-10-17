using System.Collections.Generic;
using ScriptableObjects.Surfaces;
using UnityEngine;

namespace DefaultNamespace.ScriptableObjects.Surfaces
{
    [CreateAssetMenu(fileName = "Surface Effect", menuName = "Impact System/Surface Effect")]
    public class SurfaceEffect : ScriptableObject
    {
        public List<SpawnObjectEffect> SpawnObjectEffects = new List<SpawnObjectEffect>();
        public List<PlayAudioEffect> PlayAudioEffects = new List<PlayAudioEffect>();
    }
}