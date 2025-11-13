using System;
using Pool.Core;
using UnityEngine;

namespace UI.Popup
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