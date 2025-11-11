using System;
using Pool.Configs;
using UnityEngine;

namespace Pool
{
    public interface IPoolable
    {
        public PoolKeyConfig PoolKey { get; set; }
        public GameObject GameObject { get; }
        public event Action<IPoolable> ReturnedToPool;
        public void OnSpawned();
        public void OnDespawned();
    }
}