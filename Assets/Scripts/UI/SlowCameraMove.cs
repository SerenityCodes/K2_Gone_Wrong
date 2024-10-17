using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class SlowCameraMove : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(_camera.transform.position.x, _camera.transform.position.y, 10000f), Time.deltaTime * 0.000001f);
        }
    }
}