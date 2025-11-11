using System.Collections.Generic;
using Pool.Configs;
using UnityEngine;

namespace Pool
{
    public sealed class Pool
    {
        private readonly Stack<IPoolable> _pooledObjects = new Stack<IPoolable>();

        private readonly PoolConfig _config;
        private readonly Transform _parent;

        public Pool(PoolConfig config, Transform parent = null)
        {
            _config = config;
            _parent = parent;

            for (int i = 0; i < config.PrewarmAmount; i++)
            {
                var go = Object.Instantiate(config.Prefab, parent);
                go.SetActive(false);

                var poolable = go.GetComponent<BasePooledObject>();
                _pooledObjects.Push(poolable);
            }
        }

        public T Get<T>(out IPoolable poolable) where T : Component
        {
            if (_pooledObjects.Count > 0)
            {
                poolable = _pooledObjects.Pop();
            }
            else
            {
                var go = Object.Instantiate(_config.Prefab, _parent);
                poolable = go.GetComponent<IPoolable>();
            }

            poolable.PoolKey = _config.KeyConfig;
            poolable.GameObject.SetActive(true);
            poolable.OnSpawned();

            if (poolable.GameObject.TryGetComponent(out T component))
            {
                return component;
            }

            return null;
        }

        public void Release(IPoolable pooledObject)
        {
            // pooledObject.OnDespawned();
            pooledObject.GameObject.SetActive(false);
            _pooledObjects.Push(pooledObject);
        }
    }
}