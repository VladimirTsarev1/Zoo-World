using System;
using System.Threading;
using Animals.Collision;
using Animals.Configs;
using Animals.Factory;
using Animals.Viewport;
using CameraBounds;
using Cysharp.Threading.Tasks;
using Pool.Core;
using Root;
using UI.EatenAnimalsCounters.Service;
using UI.Popup.Service;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animals.Spawn
{
    public sealed class AnimalSpawnService : IAnimalSpawnService
    {
        private readonly GameDataConfig _gameDataConfig;
        private readonly IAnimalFactory _animalFactory;
        private readonly IAnimalConfigService _animalConfigService;
        private readonly IAnimalCollisionService _animalCollisionService;
        private readonly IAnimalViewportService _animalViewportService;
        private readonly IEatenAnimalsCounterService _eatenAnimalsCounterService;
        private readonly ICameraBoundsService _cameraBoundsService;
        private readonly IPopupService _popupService;

        private CancellationTokenSource _cts;

        private float TimeToSpawn =>
            Random.Range(_gameDataConfig.TimeToSpawnAnimals.x, _gameDataConfig.TimeToSpawnAnimals.y);

        public AnimalSpawnService(
            GameDataConfig gameDataConfig,
            IAnimalFactory animalFactory,
            IAnimalConfigService animalConfigService,
            IAnimalCollisionService animalCollisionService,
            IAnimalViewportService animalViewportService,
            IEatenAnimalsCounterService eatenAnimalsCounterService,
            ICameraBoundsService cameraBoundsService,
            IPopupService popupService)
        {
            _gameDataConfig = gameDataConfig;
            _animalFactory = animalFactory;
            _animalConfigService = animalConfigService;
            _animalCollisionService = animalCollisionService;
            _animalViewportService = animalViewportService;
            _eatenAnimalsCounterService = eatenAnimalsCounterService;
            _cameraBoundsService = cameraBoundsService;
            _popupService = popupService;
        }

        public void StartSpawn()
        {
            _cts = new CancellationTokenSource();

            SpawnLoopAsync(_cts.Token).Forget();
        }

        public void StopSpawn()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private async UniTaskVoid SpawnLoopAsync(CancellationToken cancellationToken)
        {
            var spanInterval = TimeSpan.FromSeconds(TimeToSpawn);

            while (!cancellationToken.IsCancellationRequested)
            {
                var isCancelled = await UniTask
                    .Delay(spanInterval, cancellationToken: cancellationToken)
                    .SuppressCancellationThrow();

                if (isCancelled)
                {
                    break;
                }

                var spawnPosition = _cameraBoundsService.GetRandomPointOnFloor();
                spawnPosition.y += 1f;

                var randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

                var animal = _animalFactory.CreateAnimal(
                    _animalConfigService.GetRandomAnimal(),
                    _animalCollisionService,
                    _animalViewportService,
                    spawnPosition,
                    randomRotation);

                animal.ReturnedToPool += HandleReturnedToPool;

                animal.AteAnotherAnimal += HandleAteAnotherAnimal;
                animal.WasEatenByAnotherAnimal += HandleWasEatenByAnotherAnimal;
            }
        }

        private void HandleReturnedToPool(IPoolable poolObject)
        {
            poolObject.ReturnedToPool -= HandleReturnedToPool;

            if (poolObject is Animal animal)
            {
                animal.AteAnotherAnimal -= HandleAteAnotherAnimal;
                animal.WasEatenByAnotherAnimal -= HandleWasEatenByAnotherAnimal;
            }
        }

        private void HandleAteAnotherAnimal(Animal originalAnimal, Animal eatenAnimal)
        {
            var spawnPopupLabelPosition = originalAnimal.transform.position;
            spawnPopupLabelPosition.y += 1f;

            _popupService.SpawnPopupLabel(spawnPopupLabelPosition);
        }

        private void HandleWasEatenByAnotherAnimal(Animal originalAnimal, Animal eaterAnimal)
        {
            _eatenAnimalsCounterService.AnimalEaten(originalAnimal);
        }
    }
}