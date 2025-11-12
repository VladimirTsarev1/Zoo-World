using System;
using Pool.Configs;
using UnityEngine;

namespace Pool
{
    [DisallowMultipleComponent]
    public abstract class BasePooledObject : MonoBehaviour, IPoolable
    {
        public PoolKeyConfig PoolKey { get; set; }
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> ReturnedToPool;

        private void OnDisable()
        {
            OnDespawned();
        }

        public virtual void OnSpawned()
        {
        }

        public virtual void OnDespawned()
        {
            ReturnedToPool?.Invoke(this);
        }
    }
}