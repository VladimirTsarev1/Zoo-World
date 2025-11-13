using Pool.Configs;
using Pool.Core;
using UnityEngine;

namespace Pool.Service
{
    public interface IPoolService
    {
        public T Get<T>(PoolKeyConfig keyConfig, float timeToRelease = float.NaN) where T : Component;
        public void Release(IPoolable poolable);
    }
}