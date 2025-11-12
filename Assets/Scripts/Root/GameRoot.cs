using Animals.Collision;
using Animals.Configs;
using Animals.Factory;
using Animals.Spawn;
using CameraBounds;
using Pool.Configs;
using Pool.Service;
using UI.PopupService;
using UnityEngine;

namespace Root
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private GameDataConfig gameDataConfig;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private PoolKeyConfig popupLabelPoolKey;

        private IPoolService _poolService;
        private ICameraService _cameraService;

        private IAnimalConfigService _animalConfigService;
        private IAnimalCollisionService _animalCollisionService;
        private IAnimalSpawnService _animalSpawnService;
        private IAnimalFactory _animalFactory;

        private IPopupService _popupService;

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

            _cameraService = new CameraService(mainCamera);
            
            _popupService = new PopupService(_poolService, popupLabelPoolKey);

            _animalConfigService = new AnimalConfigService();

            _animalCollisionService = new AnimalCollisionService();

            _animalFactory = new AnimalFactory(_poolService);
            
            _animalSpawnService = new AnimalSpawnService(
                gameDataConfig,
                _animalFactory,
                _animalConfigService,
                _animalCollisionService,
                _cameraService,
                _popupService);
        }
    }
}