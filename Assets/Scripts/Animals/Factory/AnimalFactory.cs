using Animals.Collision;
using Animals.Configs;
using Animals.Viewport;
using CameraBounds;
using Pool.Service;
using UnityEngine;

namespace Animals.Factory
{
    public sealed class AnimalFactory : IAnimalFactory
    {
        private readonly IPoolService _poolService;
        private readonly ICameraService _cameraService;

        public AnimalFactory(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public Animal CreateAnimal(
            AnimalConfig config,
            IAnimalCollisionService collisionService,
            IAnimalViewportService viewportService,
            Vector3 spawnPosition,
            Quaternion spawnRotation = default)
        {
            var keyConfig = config.PoolKeyConfig;
            var animalComponent = _poolService.Get<Animal>(keyConfig);

            animalComponent.transform.position = spawnPosition;
            animalComponent.transform.rotation = spawnRotation;

            animalComponent.Initialize(config, collisionService, viewportService);

            return animalComponent;
        }

        private void HandleAteAnotherAnimal()
        {
        }
    }
}