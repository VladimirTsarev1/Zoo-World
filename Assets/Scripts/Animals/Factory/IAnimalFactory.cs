using Animals.Configs;
using UnityEngine;

namespace Animals.Factory
{
    public interface IAnimalFactory
    {
        public Animal CreateAnimal(AnimalConfig config, Vector3 spawnPosition, Quaternion spawnRotation = default);
    }
}