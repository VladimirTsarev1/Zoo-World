using System;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Collision;
using ZooWorld.Animals.Configs;
using ZooWorld.Animals.Move;
using ZooWorld.Animals.Viewport;
using IPoolable = ZooWorld.Pool.Core.IPoolable;

namespace ZooWorld.Animals
{
    public abstract class Animal : MonoBehaviour, IPoolable
    {
        public event Action<IPoolable> ReturnedToPool;

        public GameObject GameObject => gameObject;

        public AnimalConfig Config { get; private set; }
        public Transform ThisTransform { get; private set; }
        public bool IsOutsideViewport { get; private set; }

        protected Rigidbody Rigidbody;

        private IMoveStrategy _moveStrategy;
        private IAnimalCollisionService _collisionService;
        private IAnimalViewportService _viewportService;

        [Inject]
        public void Construct(IAnimalCollisionService collisionService, IAnimalViewportService viewportService)
        {
            _collisionService = collisionService;
            _viewportService = viewportService;
        }

        public void Initialize(AnimalConfig config)
        {
            ThisTransform = transform;
            Rigidbody = GetComponent<Rigidbody>();

            Config = config;

            _moveStrategy = config.MoveConfig.CreateStrategy();
        }

        private void Update()
        {
            _viewportService.CheckAnimalIsOutsideViewport(this);
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

        public virtual void Eat(Animal prey)
        {
        }

        public virtual void WasEaten(Animal predator)
        {
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