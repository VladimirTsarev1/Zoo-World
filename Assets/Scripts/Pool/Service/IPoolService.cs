using UnityEngine;
using ZooWorld.Pool.Configs;
using ZooWorld.Pool.Core;

namespace ZooWorld.Pool.Service
{
    public interface IPoolService
    {
        public T Get<T>(PoolKeyConfig keyConfig, float timeToRelease = float.NaN) where T : Component;
        public void Release(IPoolable poolable);
    }
}