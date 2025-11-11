using Pool.Configs;
using UnityEngine;

namespace Pool.Service
{
    public interface IPoolService
    {
        public T Get<T>(PoolKeyConfig keyConfig) where T : Component;
        public void Release(IPoolable poolable);
    }
}