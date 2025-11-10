using System.Collections.Generic;

namespace Services.Pools
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();

        public PoolService()
        {
            
        }

        public void Get<T>()
        {
            
        }

        public void Release()
        {
            
        }
    }
}