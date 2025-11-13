using System;
using UnityEngine;

namespace Pool.Core
{
    public interface IPoolable
    {
        public GameObject GameObject { get; }
        public event Action<IPoolable> ReturnedToPool;
        public void OnSpawned();
        public void OnDespawned();
    }
}