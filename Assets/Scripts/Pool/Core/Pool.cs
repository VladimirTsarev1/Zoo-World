using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Pool.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool.Core
{
    public sealed class Pool
    {
        private readonly Stack<IPoolable> _poolObjects = new();
        private readonly Dictionary<IPoolable, CancellationTokenSource> _timerCancellationTokenSources = new();

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

                var poolObject = go.GetComponent<IPoolable>();
                _poolObjects.Push(poolObject);
            }
        }

        public T Get<T>(out IPoolable poolObject, float timeToRelease = float.NaN) where T : Component
        {
            poolObject = _poolObjects.Count > 0 ? _poolObjects.Pop() : CreateNewPoolObject();

            if (poolObject == null)
            {
                Debug.LogError("Pool object is null");
                return null;
            }

            poolObject.ReturnedToPool += HandleReturnedToPool;
            poolObject.GameObject.SetActive(true);
            poolObject.OnSpawned();

            CalculateReleaseDelay(ref timeToRelease);

            if (!float.IsNaN(timeToRelease))
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                _timerCancellationTokenSources[poolObject] = cts;
                RunReleaseTimerAsync(poolObject, cts.Token, timeToRelease).Forget();
            }

            if (poolObject.GameObject.TryGetComponent(out T component))
            {
                return component;
            }

            Debug.LogError(
                $"Pool object {poolObject.GameObject.name} doesn't contain component: {typeof(T)}");

            return null;
        }

        private IPoolable CreateNewPoolObject()
        {
            var spawnedObject = Object.Instantiate(_config.Prefab, _parent);

            if (spawnedObject.TryGetComponent(out IPoolable poolable))
            {
                return poolable;
            }

            Debug.LogError($"Spawned object isn't {nameof(IPoolable)}");

            return null;
        }

        public void Release(IPoolable poolObject)
        {
            if (_timerCancellationTokenSources.TryGetValue(poolObject, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
                _timerCancellationTokenSources.Remove(poolObject);
            }

            _poolObjects.Push(poolObject);
        }

        private async UniTaskVoid RunReleaseTimerAsync(
            IPoolable poolObject,
            CancellationToken cancellationToken,
            float delay)
        {
            var isCancelled = await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken)
                .SuppressCancellationThrow();

            if (isCancelled)
            {
                return;
            }

            poolObject.GameObject.SetActive(false);
        }

        private void CalculateReleaseDelay(ref float timeToRelease)
        {
            if (float.IsNaN(timeToRelease) && _config.ReleaseCondition == PoolReleaseConditions.Timer)
            {
                timeToRelease = _config.TimeToRelease;
            }
        }

        private void HandleReturnedToPool(IPoolable poolObject)
        {
            poolObject.ReturnedToPool -= HandleReturnedToPool;
            poolObject.OnDespawned();
        }
    }
}