using System.Collections.Generic;

namespace Services.Pools
{
    public sealed class Pool
    {
        private Stack<Pool> _pooledObjects = new Stack<Pool>();
    }
}