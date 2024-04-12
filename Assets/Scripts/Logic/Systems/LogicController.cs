using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.General.Controller;
using MyGame.General.Data;
using MyGame.General.Service;
using MyGame.General.Service.Input;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Components.Input;
using MyGame.Logic.Components.Skills;
using MyGame.Logic.Services;
using MyGame.Logic.Services.Extensions;
using MyGame.Logic.Services.Views;
using MyGame.Logic.Systems.Game;
using MyGame.Logic.Systems.Hero;
using MyGame.Logic.Systems.Initialize;
using MyGame.Logic.Systems.Minotaur;

namespace MyGame.Logic.Systems
{
    public sealed class LogicController : ILogicController, IUpdate, IFixedUpdate, IDestroy
    {
        private readonly IControllersMediator _mediator;
        private readonly ViewService _viewService;

        private EcsWorld _eventWorld;
        private EcsWorld _world;
        private EcsSystems _updateSystems;
        private EcsSystems _fixedUpdateSystems;

        public LogicController(IInputService inputService, IControllersMediator mediator, IPoolService poolService,
            ITimeService timeService, ILevelStaticData levelData, IStaticData staticData, IRuntimeData runtimeData,
            ISceneSerializedData sceneSerializedData)
        {
            _mediator = mediator;
            _world = new EcsWorld();
            _eventWorld = new EcsWorld();
            _viewService = new ViewService(poolService, _world, _eventWorld);
            InitializeScene(levelData, staticData, sceneSerializedData);

            _updateSystems = new EcsSystems(_world);
            _updateSystems
                .AddWorld(_eventWorld, WorldNames.EVENT)
                .Add(new InputUpdateSystem(inputService))
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldNames.EVENT))
#endif
                .Inject();

            _fixedUpdateSystems = new EcsSystems(_world);
            _fixedUpdateSystems
                .AddWorld(_eventWorld, WorldNames.EVENT)
                .Add(new HeroInputMovementSystem())
                .Add(new MaterialSpriteUpdateSystem())
                .Add(new MediatorEventsSystem(_mediator))
                .Add(new HeroMovementSystem(timeService, runtimeData))
                .Add(new HeroDustOnMovementSystem())
                .Add(new HeroActionInputSystem(timeService, runtimeData))
                .Add(new HeroAnimateSystem())
                .Add(new HeroOnTriggerEnterSystem())
                .Add(new HarpPickedUpSystem(_mediator))
                .Add(new MinotaurActivateSystem())
                .Add(new MinotaurDestinationSetSystem())
                .Add(new MinotaurAnimateSystem())
                .Add(new MinotaurDestroySystem())
                .Add(new SpikesAnimateSystem(timeService, runtimeData))
                .Add(new WinGameSystem(_mediator))
                .Add(new GameOverSystem(_mediator))
                .Add(new DestroyViewSystem(_viewService))
                .DelayRemoveComponentSystem<DashComponent>(timeService)
                .DelayRemoveComponentSystem<CooldownComponent>(timeService)
                .OneFrameSystem<InputMovementDirectionEvent>(WorldNames.EVENT)
                .OneFrameSystem<InputActionEvent>(WorldNames.EVENT)
                .OneFrameSystem<HarpPickedUpEvent>(WorldNames.EVENT)
                .OneFrameSystem<EndGameTriggerReachedEvent>(WorldNames.EVENT)
                .OneFrameSystem<MinotaurActivateEvent>(WorldNames.EVENT)
                .OneFrameSystem<GameOverEvent>(WorldNames.EVENT)
                .OneFrameSystem<OnTriggerEnter2DEvent>(WorldNames.EVENT)
                .Inject();
            _updateSystems.Init();
            _fixedUpdateSystems.Init();
        }

        private void InitializeScene(ILevelStaticData levelData, IStaticData staticData,
            ISceneSerializedData sceneSerializedData)
        {
            var initSystems = new EcsSystems(_world);
            initSystems
                .AddWorld(_eventWorld, WorldNames.EVENT)
                .Add(new InitializeOnSceneTriggers(sceneSerializedData))
                .Add(new InitializeHeroSystem(_viewService, _mediator, levelData, staticData))
                .Add(new InitializeMinotaurSystem(_viewService, levelData, staticData))
                .Add(new InitializeArtifactSystem(_viewService, levelData, staticData))
                .Inject()
                .Init();
            initSystems.Destroy();
            _mediator.SceneInitialized();
        }

        public void Update() => _updateSystems.Run();

        public void FixedUpdate() => _fixedUpdateSystems.Run();

        public void Destroy()
        {
            if (_updateSystems != null)
            {
                _updateSystems.Destroy();
                _updateSystems = null;
            }

            if (_fixedUpdateSystems != null)
            {
                _fixedUpdateSystems.Destroy();
                _fixedUpdateSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }

            if (_eventWorld != null)
            {
                _eventWorld.Destroy();
                _eventWorld = null;
            }
        }
    }
}