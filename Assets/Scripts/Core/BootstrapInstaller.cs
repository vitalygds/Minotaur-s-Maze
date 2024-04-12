using MyGame.Core.GameControllers;
using MyGame.Core.Services;
using MyGame.Core.Services.Input;
using MyGame.Core.Services.Pooling;
using MyGame.General.Controller;
using MyGame.General.Service;
using MyGame.General.Service.Input;
using Zenject;

namespace MyGame.Core
{
    internal sealed class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();
            Container.Bind<IDataService>().To<DataService>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IControllersService>().To<ControllersService>().AsSingle();
            BindInAppServices();
            Container.Bind<IControllersMediator>().To<ControllersMediator>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }

        private void BindInAppServices()
        {
            Container.Bind<IAnalyticsService>().To<UnityAnalyticsService>().AsSingle();
            Container.Bind<IInAppServicesController>().To<InAppServicesController>().AsSingle();
        }
    }
}