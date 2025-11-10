using UnityEngine;

namespace Services.CameraBounds
{
    public interface ICameraBoundsService
    {
        bool IsOutside(Vector3 worldPoint, float offset = 0f);
    }
}