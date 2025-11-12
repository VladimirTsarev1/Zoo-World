using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Pool.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public sealed class Pool
    {
        private readonly Stack<IPoolable> _pooledObjects = new();
        private readonly Dictionary<IPoolable, CancellationTokenSource> _cancellationTokenSources = new();

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

        public T Get<T>(out IPoolable pooledObject) where T : Component
        {
            if (_pooledObjects.Count > 0)
            {
                pooledObject = _pooledObjects.Pop();
            }
            else
            {
                var go = Object.Instantiate(_config.Prefab, _parent);
                pooledObject = go.GetComponent<IPoolable>();
            }

            switch (_config.ReleaseCondition)
            {
                case PoolReleaseConditions.Timer:

                    var cts = new CancellationTokenSource();
                    _cancellationTokenSources[pooledObject] = cts;
                    ReleaseAfterDelayAsync(pooledObject, _config.TimeToRelease, cts.Token).Forget();

                    break;
            }

            pooledObject.PoolKey = _config.KeyConfig;
            pooledObject.GameObject.SetActive(true);
            pooledObject.OnSpawned();

            if (pooledObject.GameObject.TryGetComponent(out T component))
            {
                return component;
            }

            return null;
        }

        public void Release(IPoolable pooledObject)
        {
            if (_cancellationTokenSources.TryGetValue(pooledObject, out var cts))
            {
                cts?.Cancel();
                cts?.Dispose();
                _cancellationTokenSources.Remove(pooledObject);
            }

            pooledObject.GameObject.SetActive(false);
            _pooledObjects.Push(pooledObject);
        }

        private async UniTaskVoid ReleaseAfterDelayAsync(IPoolable pooledObject, float delay, CancellationToken token)
        {
            var isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token)
                .SuppressCancellationThrow();

            if (isCancelled)
            {
                return;
            }

            Release(pooledObject);
        }
    }
}