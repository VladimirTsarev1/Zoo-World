using System;
using Animals.Collision;
using Animals.Configs;
using Animals.Move;
using Pool;
using UnityEngine;

namespace Animals
{
    public abstract class Animal : BasePooledObject
    {
        public event Action<Animal, Animal> AteAnotherAnimal;
        public event Action<Animal, Animal> WasEatenByAnotherAnimal;

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

        public virtual void Ate(Animal anotherAnimal)
        {
            AteAnotherAnimal?.Invoke(this, anotherAnimal);
        }

        public virtual void Eaten(Animal eaterAnimal)
        {
            WasEatenByAnotherAnimal?.Invoke(this, eaterAnimal);

            gameObject.SetActive(false);
        }

        public virtual void Push(Vector3 pushVector, ForceMode forceMode)
        {
            Rigidbody.AddForce(pushVector, forceMode);
        }
    }
}