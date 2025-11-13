using System;
using Animals.Collision;
using Animals.Configs;
using Animals.Move;
using Animals.Viewport;
using Pool.Core;
using UnityEngine;

namespace Animals
{
    public abstract class Animal : MonoBehaviour, IPoolable
    {
        public event Action<IPoolable> ReturnedToPool;

        public event Action<Animal, Animal> AteAnotherAnimal;
        public event Action<Animal, Animal> WasEatenByAnotherAnimal;
        
        public GameObject GameObject => gameObject;

        public AnimalConfig Config { get; private set; }
        public Transform ThisTransform { get; private set; }
        public bool IsOutsideViewport { get; private set; }

        protected Rigidbody Rigidbody;

        private IMoveStrategy _moveStrategy;
        private IAnimalCollisionService _collisionService;
        private IAnimalViewportService _viewportService;

        public void Initialize(
            AnimalConfig config,
            IAnimalCollisionService collisionService,
            IAnimalViewportService viewportService)
        {
            ThisTransform = transform;
            Rigidbody = GetComponent<Rigidbody>();

            Config = config;
            _collisionService = collisionService;
            _viewportService = viewportService;

            _moveStrategy = config.MoveConfig.CreateStrategy();
        }

        private void Update()
        {
            _viewportService.CheckIsAnimalOutsideViewport(this);
        }

        protected virtual void FixedUpdate()
        {
            _moveStrategy?.Move(Rigidbody);
        }

        protected void OnDisable()
        {
            ReturnedToPool?.Invoke(this);
        }

        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (other.transform.TryGetComponent(out Animal anotherAnimal)
                && gameObject.GetInstanceID() > anotherAnimal.gameObject.GetInstanceID())
            {
                _collisionService.HandleCollision(this, anotherAnimal, other);
            }
        }

        public virtual void OnSpawned()
        {
        }

        public virtual void OnDespawned()
        {
        }

        public virtual void Ate(Animal anotherAnimal)
        {
            AteAnotherAnimal?.Invoke(this, anotherAnimal);
        }

        public virtual void WasEaten(Animal eaterAnimal)
        {
            WasEatenByAnotherAnimal?.Invoke(this, eaterAnimal);

            gameObject.SetActive(false);
        }

        public virtual void Push(Vector3 pushVector, ForceMode forceMode)
        {
            Rigidbody.AddForce(pushVector, forceMode);
        }

        public void SetOutsideViewportState(bool state)
        {
            IsOutsideViewport = state;
        }
    }
}