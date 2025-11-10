using Services.CameraBounds;
using Services.Pools;

namespace Services
{
    public static class ServiceLocator
    {
        public static IPoolService PoolService { get; set; }
        public static ICameraBoundsService CameraBounds { get; set; }
    }
}