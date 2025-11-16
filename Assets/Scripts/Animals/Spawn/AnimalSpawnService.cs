using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Collision;
using ZooWorld.Animals.Configs;
using ZooWorld.Animals.Factory;
using ZooWorld.Animals.Viewport;
using ZooWorld.CameraBounds;
using ZooWorld.Pool.Core;
using ZooWorld.Root;
using ZooWorld.UI.Popup.Service;
using Random = UnityEngine.Random;

namespace ZooWorld.Animals.Spawn
{
    public sealed class AnimalSpawnService : IAnimalSpawnService
    {
        private readonly IAnimalFactory _animalFactory;
        private readonly IAnimalConfigProvider _animalConfigProvider;

        private readonly GameDataConfig _gameDataConfig;
        private readonly CameraBoundsService _cameraBoundsService;

        private CancellationTokenSource _cts;

        private float TimeToSpawn =>
            Random.Range(_gameDataConfig.TimeToSpawnAnimals.x, _gameDataConfig.TimeToSpawnAnimals.y);

        public AnimalSpawnService(
            GameDataConfig gameDataConfig,
            IAnimalFactory animalFactory,
            IAnimalConfigProvider animalConfigProvider,
            CameraBoundsService cameraBoundsService)
        {
            _gameDataConfig = gameDataConfig;
            _animalFactory = animalFactory;
            _animalConfigProvider = animalConfigProvider;

            _cameraBoundsService = cameraBoundsService;
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
            _cts = null;
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

                _animalFactory.CreateAnimal(_animalConfigProvider.GetRandomAnimal(), spawnPosition, randomRotation);
            }
        }
    }
}