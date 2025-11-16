using System;
using System.Collections.Generic;
using ZooWorld.Animals;

namespace ZooWorld.UI.EatenAnimalsCounters
{
    public sealed class EatenAnimalsCountersService
    {
        public event Action<AnimalType, int> AmountChanged;

        private readonly Dictionary<AnimalType, int> _amounts = new()
        {
            { AnimalType.Prey, 0 },
            { AnimalType.Predator, 0 }
        };

        public void AddAnimal(AnimalType eatenAnimalType)
        {
            _amounts[eatenAnimalType]++;

            AmountChanged?.Invoke(eatenAnimalType, _amounts[eatenAnimalType]);
        }

        public int GetAmount(AnimalType type) => _amounts[type];
    }
}