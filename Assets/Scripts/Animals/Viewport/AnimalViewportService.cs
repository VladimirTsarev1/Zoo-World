using CameraBounds;
using Unity.Mathematics;

namespace Animals.Viewport
{
    public sealed class AnimalViewportService : IAnimalViewportService
    {
        private ICameraBoundsService _cameraBoundsService;

        public AnimalViewportService(ICameraBoundsService cameraBoundsService)
        {
            _cameraBoundsService = cameraBoundsService;
        }

        public void CheckIsAnimalOutsideViewport(Animal animal)
        {
            if (_cameraBoundsService.IsOutside(animal.ThisTransform.position) && !animal.IsOutsideViewport)
            {
                animal.ThisTransform.rotation *= quaternion.Euler(0f, 180f, 0f);
                animal.SetOutsideViewportState(true);
            }
            else if (!_cameraBoundsService.IsOutside(animal.ThisTransform.position) && animal.IsOutsideViewport)
            {
                animal.SetOutsideViewportState(false);
            }
        }
    }
}