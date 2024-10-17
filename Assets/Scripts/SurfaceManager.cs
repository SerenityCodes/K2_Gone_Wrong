using System.Collections.Generic;
using DefaultNamespace.ScriptableObjects.Surfaces;
using ScriptableObjects.Surfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public struct SurfaceType
    {
        public Texture Albedo;
        public Surface Surface;
    }
    
    public class SurfaceManager : MonoBehaviour
    {
        public static SurfaceManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        
        private List<SurfaceType> _surfaces = new();
        private const int DefaultPoolSizes = 10;
        private Surface defaultSurface;

        public void HandleImpact(GameObject hitObject, Vector3 hitPoint, Vector3 hitNormal, ImpactType impactType,
            int triangleIndex)
        {
            if (hitObject.TryGetComponent<Renderer>(out var renderer))
            {
                
            }
        }
        
        
    }
}