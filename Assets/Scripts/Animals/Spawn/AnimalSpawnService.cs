using System;
using System.Threading;
using Animals.Configs;
using Animals.Factory;
using CameraBounds;
using Cysharp.Threading.Tasks;
using Root;
using Random = UnityEngine.Random;

namespace Animals.Spawn
{
    public class AnimalSpawnService : IAnimalSpawnService
    {
        private readonly GameDataConfig _gameDataConfig;
        private readonly IAnimalFactory _animalFactory;
        private readonly IAnimalConfigService _animalConfigService;
        private readonly ICameraBoundsService _cameraBoundsService;

        private CancellationTokenSource _cts;

        private float TimeToSpawn =>
            Random.Range(_gameDataConfig.TimeToSpawnAnimals.x, _gameDataConfig.TimeToSpawnAnimals.y);

        public AnimalSpawnService(
            GameDataConfig gameDataConfig,
            IAnimalFactory animalFactory,
            IAnimalConfigService animalConfigService,
            ICameraBoundsService cameraBoundsService)
        {
            _gameDataConfig = gameDataConfig;
            _animalFactory = animalFactory;
            _animalConfigService = animalConfigService;
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

                _animalFactory.CreateAnimal(_animalConfigService.GetRandomAnimal(),
                    _cameraBoundsService.GetRandomPointOnFloor());
            }
        }
    }
}