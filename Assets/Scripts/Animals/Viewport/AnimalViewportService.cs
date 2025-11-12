using CameraBounds;
using Unity.Mathematics;

namespace Animals.Viewport
{
    public class AnimalViewportService : IAnimalViewportService
    {
        private ICameraService _cameraService;

        public AnimalViewportService(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void CheckIsAnimalOutsideViewport(Animal animal)
        {
            if (_cameraService.IsOutside(animal.ThisTransform.position) && !animal.IsOutsideViewport)
            {
                animal.ThisTransform.rotation *= quaternion.Euler(0f, 180f, 0f);
                animal.SetOutsideViewportState(true);
            }
            else if (!_cameraService.IsOutside(animal.ThisTransform.position) && animal.IsOutsideViewport)
            {
                animal.SetOutsideViewportState(false);
            }
        }
    }
}