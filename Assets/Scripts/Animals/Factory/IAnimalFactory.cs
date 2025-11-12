using Animals.Collision;
using Animals.Configs;
using UnityEngine;

namespace Animals.Factory
{
    public interface IAnimalFactory
    {
        public Animal CreateAnimal(
            AnimalConfig config,
            IAnimalCollisionService collisionService,
            Vector3 spawnPosition,
            Quaternion spawnRotation = default);
    }
}