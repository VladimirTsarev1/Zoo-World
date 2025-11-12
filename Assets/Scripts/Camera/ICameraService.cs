using UnityEngine;

namespace CameraBounds
{
    public interface ICameraService
    {
        public bool IsOutside(Vector3 worldPoint, float offset = 0f);
        public Vector3 GetRandomPointOnFloor();
    }
}