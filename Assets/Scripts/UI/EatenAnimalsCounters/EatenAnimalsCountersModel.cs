using System;
using Animals;

namespace UI.EatenAnimalsCounters
{
    public sealed class EatenAnimalsCountersModel
    {
        private int _preyEatenAmount;
        private int _predatorsEatenAmount;

        public event Action<int, int> EatenAmountsChanged;

        public void AddEatenAnimal(Animal eatenAnimal)
        {
            switch (eatenAnimal.Config.AnimalType)
            {
                case AnimalType.Prey:
                    _preyEatenAmount++;
                    break;
                case AnimalType.Predator:
                    _predatorsEatenAmount++;
                    break;
            }

            EatenAmountsChanged?.Invoke(_preyEatenAmount, _predatorsEatenAmount);
        }

        public void Reset()
        {
            _preyEatenAmount = 0;
            _predatorsEatenAmount = 0;

            EatenAmountsChanged?.Invoke(_preyEatenAmount, _predatorsEatenAmount);
        }
    }
}