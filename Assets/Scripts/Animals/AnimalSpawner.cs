using Pool.Service;

namespace Animals
{
    public class AnimalSpawner
    {
        private IPoolService _poolService;

        public AnimalSpawner(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public void StartSpawn()
        {
        }
    }
}