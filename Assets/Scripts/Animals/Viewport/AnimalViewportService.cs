using UnityEngine;
using ZooWorld.CameraBounds;

namespace ZooWorld.Animals.Viewport
{
    public sealed class AnimalViewportService : IAnimalViewportService
    {
        private CameraBoundsService _cameraBoundsService;

        public AnimalViewportService(CameraBoundsService cameraBoundsService)
        {
            _cameraBoundsService = cameraBoundsService;
        }

        public void CheckAnimalIsOutsideViewport(Animal animal)
        {
            if (_cameraBoundsService.IsOutside(animal.ThisTransform.position) && !animal.IsOutsideViewport)
            {
                animal.ThisTransform.rotation *= Quaternion.Euler(0f, 180f, 0f);
                animal.SetOutsideViewportState(true);
            }
            else if (!_cameraBoundsService.IsOutside(animal.ThisTransform.position) && animal.IsOutsideViewport)
            {
                animal.SetOutsideViewportState(false);
            }
        }
    }
}