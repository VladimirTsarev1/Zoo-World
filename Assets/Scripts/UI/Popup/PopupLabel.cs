using System;
using UnityEngine;
using ZooWorld.Pool.Core;

namespace ZooWorld.UI.Popup
{
    public sealed class PopupLabel : MonoBehaviour, IPoolable
    {
        public GameObject GameObject => gameObject;
        public event Action<IPoolable> ReturnedToPool;

        private void OnDisable()
        {
            ReturnedToPool?.Invoke(this);
        }

        public void OnSpawned()
        {
        }

        public void OnDespawned()
        {
        }

        public void Setup(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}