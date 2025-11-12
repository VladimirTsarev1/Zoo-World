using UnityEngine;

namespace Utilities
{
    [DisallowMultipleComponent]
    public sealed class FaceCamera : MonoBehaviour
    {
        private UnityEngine.Camera _camera;
        private Transform _cameraTransform;
        private Transform _thisTransform;

        private void Awake()
        {
            _thisTransform = transform;
        }

        private void Start()
        {
            _camera = UnityEngine.Camera.main;

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