using Animals.Collision;
using Animals.Configs;
using Pool.Service;
using UnityEngine;

namespace Animals.Factory
{
    public class AnimalFactory : IAnimalFactory
    {
        private readonly IPoolService _poolService;

        public AnimalFactory(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public Animal CreateAnimal(AnimalConfig config, IAnimalCollisionService collisionService, Vector3 spawnPosition,
            Quaternion spawnRotation = default)
        {
            var keyConfig = config.PoolKeyConfig;
            var animalComponent = _poolService.Get<Animal>(keyConfig);

            animalComponent.transform.position = spawnPosition;
            animalComponent.transform.rotation = spawnRotation;

            animalComponent.Initialize(config, collisionService);

            return animalComponent;
        }
    }
}