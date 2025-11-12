using Animals.Collision;
using Animals.Configs;
using Animals.Move;
using Pool;
using UnityEngine;

namespace Animals
{
    public abstract class Animal : BasePooledObject
    {
        public AnimalConfig Config { get; private set; }

        protected Rigidbody Rigidbody;

        private IMoveStrategy _moveStrategy;
        private IAnimalCollisionService _collisionService;

        public void Initialize(AnimalConfig config, IAnimalCollisionService collisionService)
        {
            Config = config;
            _collisionService = collisionService;

            _moveStrategy = config.MoveConfig.CreateStrategy();
            Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            _moveStrategy?.Move(Rigidbody);
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.transform.TryGetComponent(out Animal anotherAnimal)
                && gameObject.GetInstanceID() > anotherAnimal.gameObject.GetInstanceID())
            {
                _collisionService.HandleCollision(this, anotherAnimal, other);
            }
        }

        public virtual void Ate()
        {
            
        }

        public virtual void Eaten()
        {
        }

        public virtual void Push(Vector3 pushVector, ForceMode forceMode)
        {
            Rigidbody.AddForce(pushVector, forceMode);
        }
    }
}