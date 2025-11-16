using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZooWorld.Animals.Configs
{
    public sealed class AnimalConfigProvider : IAnimalConfigProvider
    {
        private readonly List<AnimalConfig> _animalConfigs;

        public AnimalConfigProvider()
        {
            _animalConfigs = Resources.LoadAll<AnimalConfig>("ScriptableObjects/AnimalConfigs").ToList();
        }

        public IReadOnlyList<AnimalConfig> GetAllConfigs()
        {
            if (_animalConfigs != null)
            {
                return _animalConfigs;
            }

            Debug.LogError("List is null");

            return null;
        }

        public AnimalConfig GetRandomAnimal()
        {
            return _animalConfigs[Random.Range(0, _animalConfigs.Count)];
        }
    }
}