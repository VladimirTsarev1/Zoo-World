using System;
using UnityEngine;

namespace ZooWorld.Pool.Core
{
    public interface IPoolable
    {
        public GameObject GameObject { get; }
        public event Action<IPoolable> ReturnedToPool;
        public void OnSpawned();
        public void OnDespawned();
    }
}