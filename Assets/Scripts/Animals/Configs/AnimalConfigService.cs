using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Animals.Configs
{
    public sealed class AnimalConfigService : IAnimalConfigService
    {
        private readonly List<AnimalConfig> _animalConfigs;

        public AnimalConfigService()
        {
            _animalConfigs = Resources.LoadAll<AnimalConfig>("ScriptableObjects/AnimalConfigs").ToList();
        }

        public AnimalConfig GetRandomAnimal()
        {
            return _animalConfigs[Random.Range(0, _animalConfigs.Count)];
        }
    }
}