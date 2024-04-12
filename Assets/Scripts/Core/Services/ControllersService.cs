using System;
using MyGame.Core.GameControllers;
using MyGame.Core.GameControllers.CameraControllers;
using MyGame.Core.Services.Input;
using MyGame.General.Controller;
using MyGame.General.Data;
using MyGame.General.Debug;
using MyGame.General.Service;
using MyGame.General.Service.Input;
using MyGame.Logic.Systems;
using UnityEngine;
using Object = UnityEngine.Object;
using Zenject;

namespace MyGame.Core.Services
{
    internal sealed class ControllersService : IControllersService
    {
        private const string c_gameController = "[GAMECONTROLLER]";
        private readonly IPoolService _poolService;
        private readonly ITimeService _timeService;
        private readonly IInputService _inputService;
        private IGameController _gameController;

        [Inject]
        public ControllersService(IPoolService poolService, ITimeService timeService, IInputService inputService)
        {
            _poolService = poolService;
            _timeService = timeService;
            _inputService = inputService;
        }

        public void CreateGameController(int controllersCapacity)
        {
            if (_gameController == null)
            {
                _gameController = new GameObject(c_gameController).AddComponent<GameController>();
                _gameController.Initialize(controllersCapacity);
                _gameController.Disable();
            }
            else
            {
                _poolService.ClearPools();
            }
            if (!Debug.isDebugBuild) return;
            var console = Object.FindObjectOfType<ConsoleToGUI>();
            console.Construct(_inputService);
        }

        public void CreateUiController(IUIData uiData, IControllersMediator mediator, bool IsJoystickUse)
        {
            IUIController uiController = new UIController(uiData, mediator, IsJoystickUse);
            _gameController.AddController<IUIController>(uiController);
        }

        public void CreateCameraController(Camera camera, ICameraData cameraData, IControllersMediator mediator)
        {
            ICameraController cameraController = new CameraController(camera, cameraData, mediator);
            _gameController.AddController<ICameraController>(cameraController);
        }

        public void CreateLogicController(ILevelStaticData levelData, IStaticData staticData, IRuntimeData runtimeData, ISceneSerializedData sceneSerializedData, IControllersMediator mediator)
        {
            ILogicController logicController = new LogicController(_inputService, mediator, _poolService, _timeService,
                levelData, staticData, runtimeData, sceneSerializedData);
            _gameController.AddController<ILogicController>(logicController);
        }

        public void StartControllers() => _gameController.Enable();

        public void DestroyLogic()
        {
            _gameController.Disable();
            _gameController.DestroyLogic();
        }
    }
}