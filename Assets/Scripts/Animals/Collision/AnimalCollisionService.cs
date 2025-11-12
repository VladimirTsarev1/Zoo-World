using System.Collections.Generic;
using System.Linq;
using Animals.Collision.Configs;
using UnityEngine;

namespace Animals.Collision
{
    public class AnimalCollisionService : IAnimalCollisionService
    {
        private readonly Dictionary<(AnimalType, AnimalType), AnimalCollisionConfig> _collisionConfigs;

        public AnimalCollisionService()
        {
            var configsList = Resources
                .LoadAll<AnimalCollisionConfig>("ScriptableObjects/AnimalConfigs/CollisionConfigs")
                .ToList();

            _collisionConfigs = new Dictionary<(AnimalType, AnimalType), AnimalCollisionConfig>();

            foreach (var config in configsList)
            {
                _collisionConfigs.Add((config.AnimalTypeA, config.AnimalTypeB), config);
            }
        }

        public void HandleCollision(Animal originalAnimal, Animal anotherAnimal,
            UnityEngine.Collision collisionData)
        {
            var originalAnimalType = originalAnimal.Config.AnimalType;
            var anotherAnimalType = anotherAnimal.Config.AnimalType;

            if (_collisionConfigs.TryGetValue((originalAnimalType, anotherAnimalType), out var config)
                || _collisionConfigs.TryGetValue((anotherAnimalType, originalAnimalType), out config))
            {
                config.HandleCollision(originalAnimal, anotherAnimal, collisionData);
                return;
            }

            Debug.LogWarning($"No Config for ({originalAnimalType}, {anotherAnimalType})");
        }
    }
}