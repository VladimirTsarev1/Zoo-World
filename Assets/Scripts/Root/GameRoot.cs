using Animals.Collision;
using Animals.Configs;
using Animals.Factory;
using Animals.Spawn;
using Animals.Viewport;
using CameraBounds;
using Pool.Configs;
using Pool.Service;
using UI.EatenAnimalsCounters;
using UI.EatenAnimalsCounters.Service;
using UI.Popup.Service;
using UnityEngine;

namespace Root
{
    [DisallowMultipleComponent]
    public sealed class GameRoot : MonoBehaviour
    {
        [SerializeField] private GameDataConfig gameDataConfig;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private EatenAnimalsCountersView eatenAnimalsCountersView;

        [SerializeField] private PoolKeyConfig popupLabelPoolKey;

        private IPoolService _poolService;
        private ICameraBoundsService _cameraBoundsService;

        private IAnimalConfigService _animalConfigService;
        private IAnimalCollisionService _animalCollisionService;
        private IAnimalViewportService _animalViewportService;
        private IAnimalSpawnService _animalSpawnService;
        private IAnimalFactory _animalFactory;

        private IPopupService _popupService;

        private IEatenAnimalsCounterService _eatenAnimalsCounterService;

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

            _cameraBoundsService = new CameraBoundsBoundsService(mainCamera);

            _popupService = new PopupService(_poolService, popupLabelPoolKey);

            _eatenAnimalsCounterService = new EatenAnimalsCounterService(eatenAnimalsCountersView);

            _animalConfigService = new AnimalConfigService();

            _animalCollisionService = new AnimalCollisionService();

            _animalViewportService = new AnimalViewportService(_cameraBoundsService);

            _animalFactory = new AnimalFactory(_poolService);

            _animalSpawnService = new AnimalSpawnService(
                gameDataConfig,
                _animalFactory,
                _animalConfigService,
                _animalCollisionService,
                _animalViewportService,
                _eatenAnimalsCounterService,
                _cameraBoundsService,
                _popupService);
        }
    }
}