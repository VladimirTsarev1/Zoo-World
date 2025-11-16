using Zenject;
using ZooWorld.UI.EatenAnimalsCounters;

namespace ZooWorld.Animals.Entities.Snake
{
    public sealed class Snake : Animal
    {
        private EatenAnimalsCountersService _eatenAnimalsCountersService;

        [Inject]
        public void Construct(EatenAnimalsCountersService eatenAnimalsCountersService)
        {
            _eatenAnimalsCountersService = eatenAnimalsCountersService;
        }

        public override void Eat(Animal prey)
        {
            _eatenAnimalsCountersService.AddAnimal(prey.Config.AnimalType);
        }
    }
}