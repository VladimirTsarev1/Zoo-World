using Animals.Collision;
using Animals.Configs;
using Animals.Viewport;
using UnityEngine;

namespace Animals.Factory
{
    public interface IAnimalFactory
    {
        public Animal CreateAnimal(
            AnimalConfig config,
            IAnimalCollisionService collisionService,
            IAnimalViewportService viewportService,
            Vector3 spawnPosition,
            Quaternion spawnRotation = default);
    }
}