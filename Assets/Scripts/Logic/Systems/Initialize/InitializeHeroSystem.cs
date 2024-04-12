using Leopotam.EcsLite;
using MyGame.General.Controller;
using MyGame.General.Data;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services.Views;
using MyGame.Logic.Views;
using UnityEngine;

namespace MyGame.Logic.Systems.Initialize
{
    internal sealed class InitializeHeroSystem : IEcsInitSystem
    {
        private readonly IViewService _viewService;
        private readonly IControllersMediator _mediator;
        private readonly ILevelStaticData _levelData;
        private readonly IStaticData _staticData;

        public InitializeHeroSystem(IViewService viewService, IControllersMediator mediator,
            ILevelStaticData levelData, IStaticData staticData)
        {
            _viewService = viewService;
            _mediator = mediator;
            _levelData = levelData;
            _staticData = staticData;
        }
        public void Init(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var heroEntity = world.NewEntity(); 
            var hero = _viewService.CreateView<HeroView>(heroEntity, _staticData.HeroPrefab, null);
            hero.Transform.position = _levelData.HeroSpawnPoint;
            ref var heroBody = ref world.GetPool<Rigidbody2DComponent>().Get(heroEntity).Value;
            heroBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _mediator.HeroCreated(hero);
        }
    }
}