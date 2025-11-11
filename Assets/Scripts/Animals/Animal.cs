using Animals.Move;
using Pool;
using UnityEngine;

namespace Animals
{
    public abstract class Animal : BasePooledObject
    {
        protected Rigidbody Rigidbody;

        private IMoveStrategy _moveStrategy;

        public void Initialize(MoveConfig moveConfig)
        {
            _moveStrategy = moveConfig.CreateStrategy();
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Update()
        {
            _moveStrategy?.Move(Rigidbody);
        }
    }
}