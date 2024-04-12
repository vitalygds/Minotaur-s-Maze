using Leopotam.EcsLite;
using MyGame.General.Data;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services.Views;
using MyGame.Logic.Views;

namespace MyGame.Logic.Systems.Initialize
{
    internal sealed class InitializeMinotaurSystem : IEcsInitSystem
    {
        private readonly ViewService _viewService;
        private readonly ILevelStaticData _levelData;
        private readonly IStaticData _staticData;

        public InitializeMinotaurSystem(ViewService viewService, ILevelStaticData levelData, IStaticData staticData)
        {
            _viewService = viewService;
            _levelData = levelData;
            _staticData = staticData;
        }
        public void Init(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var minotaurEntity = world.NewEntity(); 
            _viewService.CreateView<MinotaurView>(minotaurEntity, _staticData.MinotaurPrefab, null);
            ref var minotaurTransform = ref world.GetPool<TransformComponent>().Get(minotaurEntity).Value;
            minotaurTransform.position = _levelData.MinotaurSpawnPoint;
            ref var minotaurGo = ref world.GetPool<GameObjectComponent>().Get(minotaurEntity).Value;
            minotaurGo.SetActive(false);
            world.GetPool<DisabledComponent>().Add(minotaurEntity);
        }
    }
}