using System.Collections.Generic;

namespace ZooWorld.Animals.Configs
{
    public interface IAnimalConfigProvider
    {
        public IReadOnlyList<AnimalConfig> GetAllConfigs();
        public AnimalConfig GetRandomAnimal();
    }
}