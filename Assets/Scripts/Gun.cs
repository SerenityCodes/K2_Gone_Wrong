using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Gun : MonoBehaviour
    {
        public float damage = 10f;
        public float range = 100f;

        public Camera fpsCamera;
        public AudioClip gunShootClip;

        private AudioSource _gunSource;
        
        private void Start()
        {
            _gunSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            _gunSource.clip = gunShootClip;
            _gunSource.loop = false;
            _gunSource.Play();
            RaycastHit hit;
            var ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    var target = hit.collider.GetComponent<Target>();
                    target.TakeDamage(damage);
                }
            }
        }
    }
}