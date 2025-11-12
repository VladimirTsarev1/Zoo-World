using System;
using UnityEngine;

namespace Utilities
{
    public class FaceCamera : MonoBehaviour
    {
        private Camera _camera;
        private Transform _cameraTransform;
        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;
        }

        private void Start()
        {
            _camera = Camera.main;

            if (_camera)
            {
                _cameraTransform = _camera.transform;
            }
        }

        private void LateUpdate()
        {
            if (!_camera)
            {
                return;
            }

            Vector3 targetPosition = _thisTransform.position + _cameraTransform.rotation * Vector3.forward;
            Vector3 upDirection = _cameraTransform.rotation * Vector3.up;

            _thisTransform.LookAt(targetPosition, upDirection);
        }
    }
}