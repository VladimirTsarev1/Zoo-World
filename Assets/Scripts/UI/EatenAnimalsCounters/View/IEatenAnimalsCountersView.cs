using ZooWorld.Animals;

namespace ZooWorld.UI.EatenAnimalsCounters.View
{
    public interface IEatenAnimalsCountersView
    {
        public void SetAmount(AnimalType animalType, int amount);
    }
}