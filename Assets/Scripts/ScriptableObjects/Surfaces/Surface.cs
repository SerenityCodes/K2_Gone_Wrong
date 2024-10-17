using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects.Surfaces;
using UnityEngine;

namespace ScriptableObjects.Surfaces
{
    public class Surface : ScriptableObject
    {
        public class SurfaceImpactTypeEffect
        {
            public ImpactType ImpactType;
            public SurfaceEffect Effect;
        }
        public List<SurfaceImpactTypeEffect> ImpactTypeEffects;
    }
}