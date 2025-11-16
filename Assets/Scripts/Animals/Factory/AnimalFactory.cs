using UnityEngine;
using Zenject;
using ZooWorld.Animals.Collision;
using ZooWorld.Animals.Configs;
using ZooWorld.Animals.Viewport;
using ZooWorld.CameraBounds;
using ZooWorld.Pool.Service;

namespace ZooWorld.Animals.Factory
{
    public sealed class AnimalFactory : IAnimalFactory
    {
        private readonly IPoolService _poolService;

        public AnimalFactory(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public Animal CreateAnimal(AnimalConfig config, Vector3 spawnPosition, Quaternion spawnRotation = default)
        {
            var keyConfig = config.PoolKeyConfig;
            var animalComponent = _poolService.Get<Animal>(keyConfig);

            animalComponent.transform.position = spawnPosition;
            animalComponent.transform.rotation = spawnRotation;

            animalComponent.Initialize(config);

            return animalComponent;
        }
    }
}