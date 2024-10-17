using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    public enum GunType
    {
        Shotgun,
        AssaultRifle,
        Rifle
    }
    
    [CreateAssetMenu(menuName = "Guns/Gun", fileName = "Gun", order = 0)]
    public class GunObject : ScriptableObject
    {
        public new string name;
        public GunType type;
        public GameObject modelPrefab;
        public Vector3 spawnPoint;
        public Vector3 spawnRotation;
        
        public ShootConfig shootConfig;
        public TrailConfig trailConfig;

        private Camera _mainCamera;
        private MonoBehaviour _activeMono;
        private GameObject _model;
        private float _lastShotTime;
        private ParticleSystem _shootSystem;
        private ObjectPool<TrailRenderer> _trailPool;

        public void Spawn(Transform parent, MonoBehaviour activeMono, Camera mainCamera)
        {
            _activeMono = activeMono;
            _lastShotTime = 0;
            _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
            _model = Instantiate(modelPrefab, parent);
            _model.transform.position = spawnPoint;
            _model.transform.rotation = Quaternion.Euler(spawnRotation);
            _mainCamera = mainCamera;
            _shootSystem = _model.GetComponentInChildren<ParticleSystem>();
        }

        public void Shoot()
        {
            if (Time.time > shootConfig.fireRate + _lastShotTime)
            {
                _lastShotTime = Time.time;
                _shootSystem.Play();
                var shootDirection = _mainCamera.transform.forward + RandomSpreadVec();
                shootDirection.Normalize();
                
                var didHit = Physics.Raycast(_mainCamera.transform.position, shootDirection, out var hit,
                    float.MaxValue,
                    shootConfig.hitMask);
                var coroutine = didHit
                    ? PlayTrail(_shootSystem.transform.position, hit.point, hit)
                    : PlayTrail(_shootSystem.transform.position, _shootSystem.transform.position + shootDirection * trailConfig.MissDistance, hit);
                _activeMono.StartCoroutine(coroutine);
            }
        }

        private IEnumerator PlayTrail(Vector3 startPoint, Vector3 endPoint, RaycastHit hit)
        {
            var renderer = _trailPool.Get();
            renderer.transform.position = startPoint;
            renderer.gameObject.SetActive(true);
            yield return null; // Wait a frame
            renderer.emitting = true;
            var distance = Vector3.Distance(startPoint, endPoint);
            var remainingDistance = distance;
            while (remainingDistance > 0.0f)
            {
                renderer.transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.Clamp01(1 - remainingDistance / distance));
                remainingDistance -= trailConfig.SimulationSpeed * Time.deltaTime;
                yield return null;
            }
            renderer.transform.position = endPoint;
            if (hit.collider != null)
            {
                // Make impact effect here
            }
            yield return new WaitForSeconds(trailConfig.Duration);
            yield return null;
            renderer.emitting = false;
            renderer.gameObject.SetActive(false);
            _trailPool.Release(renderer);
        }

        private Vector3 RandomSpreadVec()
        {
            return new Vector3(Random.Range(-shootConfig.spread.x, shootConfig.spread.x),
                Random.Range(-shootConfig.spread.y, shootConfig.spread.y),
                Random.Range(-shootConfig.spread.z, shootConfig.spread.z));
        }

        private TrailRenderer CreateTrail()
        {
            var trailInstance = new GameObject("Bullet Trail");
            var trail = trailInstance.AddComponent<TrailRenderer>();
            trail.colorGradient = trailConfig.Color;
            trail.material = trailConfig.TrailMaterial;
            trail.widthCurve = trailConfig.TrailCurve;
            trail.time = trailConfig.Duration;
            trail.minVertexDistance = trailConfig.minVertexDistance;
            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            return trail;
        }
    }
}