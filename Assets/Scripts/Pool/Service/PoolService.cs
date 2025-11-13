using System.Collections.Generic;
using System.Linq;
using Pool.Configs;
using Pool.Core;
using UnityEngine;

namespace Pool.Service
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<PoolKeyConfig, Core.Pool> _pools = new();
        private readonly Dictionary<IPoolable, Core.Pool> _activePoolObjects = new();

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

            var newPool = new Core.Pool(poolConfig, poolParentObject.transform);

            _pools.Add(poolConfig.KeyConfig, newPool);
        }

        public T Get<T>(PoolKeyConfig keyConfig, float timeToRelease = float.NaN) where T : Component
        {
            if (_pools.TryGetValue(keyConfig, out var pool))
            {
                var component = pool.Get<T>(out IPoolable pooledObject, timeToRelease);

                _activePoolObjects.Add(pooledObject, pool);

                pooledObject.ReturnedToPool += Release;

                return component;
            }

            return null;
        }

        public void Release(IPoolable pooledObject)
        {
            pooledObject.ReturnedToPool -= Release;

            if (_activePoolObjects.TryGetValue(pooledObject, out var pool))
            {
                pool.Release(pooledObject);
                _activePoolObjects.Remove(pooledObject);
            }
        }
    }
}