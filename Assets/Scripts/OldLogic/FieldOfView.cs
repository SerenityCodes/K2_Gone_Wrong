using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class FieldOfView : MonoBehaviour
    {
        public float radius;
        [Range(0, 360)] public float angle;

        private GameObject _targetReference;
        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public bool canSeePlayer;

        private void Start()
        {
            _targetReference = GameObject.FindGameObjectWithTag("Player");
            StartCoroutine(FOVRoutine());
        }

        private IEnumerator FOVRoutine()
        {
            const float delay = 0.2f;
            WaitForSeconds wait = new WaitForSeconds(delay);
            
            while (true)
            {
                yield return wait;
                FieldOfViewCheck();
            }
        }

        private void FieldOfViewCheck()
        {
            var rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
            if (rangeChecks.Length > 0)
            {
                var target = rangeChecks[0].transform;
                var directionToTarget = (target.position - transform.position).normalized;
                if (!(Vector3.Angle(transform.forward, directionToTarget) < angle / 2f)) return;
                var distanceToTarget = Vector3.Distance(transform.position, target.position);
                canSeePlayer = !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask);
            }
            else if (canSeePlayer)
            {
                canSeePlayer = false;
            }
        }
    }
}