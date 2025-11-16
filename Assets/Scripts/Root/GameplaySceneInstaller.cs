using UnityEngine;
using Zenject;
using ZooWorld.Animals.Collision;
using ZooWorld.Animals.Configs;
using ZooWorld.Animals.Factory;
using ZooWorld.Animals.Spawn;
using ZooWorld.Animals.Viewport;
using ZooWorld.CameraBounds;
using ZooWorld.Pool.Configs;
using ZooWorld.Pool.Service;

namespace ZooWorld.Root
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GameDataConfig gameDataConfig;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private PoolKeyConfig popupLabelPoolKey;

        public override void InstallBindings()
        {
            Container.Bind<CameraBoundsService>().AsSingle().WithArguments(mainCamera);
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();

            Container.Bind<IAnimalConfigProvider>().To<AnimalConfigProvider>().AsSingle().NonLazy();

            Container.Bind<IAnimalCollisionService>().To<AnimalCollisionService>().AsSingle();
            Container.Bind<IAnimalViewportService>().To<AnimalViewportService>().AsSingle();

            Container.Bind<IAnimalFactory>().To<AnimalFactory>().AsSingle();
            Container.Bind<IAnimalSpawnService>().To<AnimalSpawnService>().AsSingle().WithArguments(gameDataConfig);

            Container.BindInterfacesAndSelfTo<AnimalSpawnController>().AsSingle();

            // _popupService = new PopupService(_poolService, popupLabelPoolKey);
        }
    }
}