using UnityEngine;
using Zenject;
using ZooWorld.UI.EatenAnimalsCounters;
using ZooWorld.UI.EatenAnimalsCounters.View;

namespace ZooWorld.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private EatenAnimalsCountersView eatenAnimalsCountersView;

        public override void InstallBindings()
        {
            Container.Bind<EatenAnimalsCountersService>().AsSingle();
            Container.Bind<IEatenAnimalsCountersView>().FromInstance(eatenAnimalsCountersView).AsSingle();
            Container.Bind<EatenAnimalsCountersPresenter>().AsSingle().NonLazy();
        }
    }
}