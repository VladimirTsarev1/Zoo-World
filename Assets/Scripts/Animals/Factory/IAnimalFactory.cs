using UnityEngine;
using ZooWorld.Animals.Collision;
using ZooWorld.Animals.Configs;
using ZooWorld.Animals.Viewport;

namespace ZooWorld.Animals.Factory
{
    public interface IAnimalFactory
    {
        public Animal CreateAnimal(AnimalConfig config, Vector3 spawnPosition, Quaternion spawnRotation = default);
    }
}