using System.Collections.Generic;
using System.Linq;
using Factory;
using Pool.Configs;
using UnityEngine;

namespace Pool.Service
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<PoolKeyConfig, Pool> _pools = new Dictionary<PoolKeyConfig, Pool>();

        private IFactory _factory;

        public PoolService()
        {
            InitFactory();
            InitPools();
        }

        private void InitFactory()
        {
            _factory = new UniversalFactory();
        }

        private void InitPools()
        {
            var poolConfigsList = Resources.LoadAll<PoolConfig>("ScriptableObjects").ToList();

            for (var i = 0; i < poolConfigsList.Count; i++)
            {
                CreatePool(poolConfigsList[i]);
            }
        }

        private void CreatePool(PoolConfig poolConfig)
        {
            var poolParentObject = new GameObject(poolConfig.KeyConfig.name);

            var newPool = new Pool(_factory, poolConfig, poolParentObject.transform);

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

            return default;
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