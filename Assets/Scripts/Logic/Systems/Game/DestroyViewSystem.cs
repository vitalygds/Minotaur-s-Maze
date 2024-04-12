using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services.Views;

namespace MyGame.Logic.Systems.Game
{
    internal sealed class DestroyViewSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilterInject<Inc<ViewComponent, OnDestroyComponent>> _filter = default;
        private readonly EcsFilterInject<Inc<ViewComponent>> _onDestroyFilter = default;
        private readonly IViewService _viewService;

        public DestroyViewSystem(IViewService viewService) => _viewService = viewService;
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _viewService.DestroyView(entity);
            }
        }

        public void Destroy(EcsSystems systems)
        {
            foreach (var entity in _onDestroyFilter.Value)
            {
                _viewService.DestroyView(entity);
            }
        }
    }
}