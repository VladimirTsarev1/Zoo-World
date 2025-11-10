using UnityEngine;

namespace Services.CameraBounds
{
    public sealed class CameraBoundsService : ICameraBoundsService
    {
        private readonly Camera _camera;

        public CameraBoundsService(Camera camera)
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
    }
}