using UnityEngine;

namespace CameraBounds
{
    public sealed class CameraService : ICameraService
    {
        private readonly Camera _camera;

        private Vector3 _cameraFloorCenterPoint;

        public CameraService(Camera camera)
        {
            _camera = camera != null ? camera : Camera.main;
        }

        public bool IsOutside(Vector3 worldPoint, float offset = 0f)
        {
            if (_camera == null)
            {
                return true;
            }

            Vector3 viewportPoint = _camera.WorldToViewportPoint(worldPoint);

            float min = 0f - offset;
            float max = 1f + offset;

            return viewportPoint.x < min || viewportPoint.x > max
                                         || viewportPoint.y < min || viewportPoint.y > max;
        }

        public Vector3 GetRandomPointOnFloor()
        {
            float viewportX = Random.Range(0f, 1f);
            float viewportY = Random.Range(0f, 1f);
            float viewportZ = 0;

            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit))
            {
                if (hit.transform)
                {
                    viewportZ = hit.distance;
                }
            }
            
            Vector3 viewportPos = new Vector3(viewportX, viewportY, viewportZ);
            Vector3 worldPos = _camera.ViewportToWorldPoint(viewportPos);

            return worldPos;
        }
    }
}