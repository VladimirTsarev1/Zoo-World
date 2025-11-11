using Animals.Configs;
using Animals.Factory;
using Animals.Spawn;
using CameraBounds;
using Pool.Service;
using UnityEngine;

namespace Root
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private GameDataConfig gameDataConfig;
        [SerializeField] private Camera mainCamera;

        private IPoolService _poolService;
        private ICameraBoundsService _cameraBoundsService;

        private IAnimalConfigService _animalConfigService;
        private IAnimalSpawnService _animalSpawnService;
        private IAnimalFactory _animalFactory;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _animalSpawnService.StartSpawn();
        }

        private void OnDestroy()
        {
            _animalSpawnService.StopSpawn();
        }

        private void Init()
        {
            _poolService = new PoolService();

            _cameraBoundsService = new CameraBoundsService(mainCamera);

            _animalConfigService = new AnimalConfigService();

            _animalFactory = new AnimalFactory(_poolService);

            _animalSpawnService = new AnimalSpawnService(
                gameDataConfig,
                _animalFactory,
                _animalConfigService,
                _cameraBoundsService);
        }
    }
}