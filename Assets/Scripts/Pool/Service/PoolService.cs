using System.Collections.Generic;
using System.Linq;
using Pool.Configs;
using UnityEngine;

namespace Pool.Service
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<PoolKeyConfig, Pool> _pools = new Dictionary<PoolKeyConfig, Pool>();

        public PoolService()
        {
            InitPools();
        }

        private void InitPools()
        {
            var poolConfigsList = Resources.LoadAll<PoolConfig>("ScriptableObjects/PoolConfigs").ToList();

            for (var i = 0; i < poolConfigsList.Count; i++)
            {
                CreatePool(poolConfigsList[i]);
            }
        }

        private void CreatePool(PoolConfig poolConfig)
        {
            var poolParentObject = new GameObject(poolConfig.KeyConfig.name);

            var newPool = new Pool(poolConfig, poolParentObject.transform);

            _pools.Add(poolConfig.KeyConfig, newPool);
        }

        public T Get<T>(PoolKeyConfig keyConfig) where T : Component
        {
            if (_pools.TryGetValue(keyConfig, out var pool))
            {
                var poolObject = pool.Get<T>(out IPoolable poolable);

                poolable.ReturnedToPool += Release;

                return poolObject;
            }

            return null;
        }

        public void Release(IPoolable pooledObject)
        {
            pooledObject.ReturnedToPool -= Release;

            if (_pools.TryGetValue(pooledObject.PoolKey, out var pool))
            {
                pool.Release(pooledObject);
            }
        }
    }
}