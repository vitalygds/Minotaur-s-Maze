using Leopotam.EcsLite;
using MyGame.General.Service;
using MyGame.Logic.Components.Unity;
using UnityEngine;

namespace MyGame.Logic.Services.Views
{
    internal sealed class ViewService : IViewService
    {
        private readonly IPoolService _poolService;
        private readonly EcsWorld _defaultWorld;
        private readonly EcsWorld _eventWorld;
        private readonly EcsPool<ViewComponent> _viewPool;

        public ViewService(IPoolService poolService, EcsWorld defaultWorld, EcsWorld eventWorld)
        {
            _poolService = poolService;
            _defaultWorld = defaultWorld;
            _eventWorld = eventWorld;
            _viewPool = defaultWorld.GetPool<ViewComponent>();
        }

        public T CreateView<T>(int entity, GameObject prefab, Transform parent) where T : Component, IEcsView
        { 
            T view = _poolService.Instantiate<T>(prefab, parent);
            view.InitializeView(_defaultWorld, _eventWorld, entity);
            return view;
        }

        public void DestroyView(int entity)
        {
            ref IEcsView view = ref _viewPool.Get(entity).Value;
            _poolService.Destroy(view.GameObject);
            _defaultWorld.DelEntity(entity);
        }
    }
}