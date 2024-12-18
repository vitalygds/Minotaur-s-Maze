using System;
using General;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Core
{
    internal sealed class GameFactory : IGameFactory, IDisposable
    {
        private readonly IDataService _dataService;
        private readonly IInAppServicesController _inAppServicesController;
        private readonly IControllersService _controllersService;
        private readonly IControllersMediator _mediator;
        private IGameController _gameController;


        [Inject]
        public GameFactory(IDataService dataService, IInAppServicesController inAppServicesController,
            IControllersService controllersService, IControllersMediator mediator)
        {
            _dataService = dataService;
            _inAppServicesController = inAppServicesController;
            _controllersService = controllersService;
            _mediator = mediator;
            _mediator.OnRestartGameEvent += RestartLevel;
            _mediator.OnGameQuitEvent += OnGameQuit;
        }

        private void OnGameQuit()
        {
            _controllersService.DestroyLogic();
        }

        private void RestartLevel()
        {
            _controllersService.DestroyLogic();
            _controllersService.CreateLogicController(_dataService.LevelData(SceneManager.GetActiveScene().name),
                _dataService.StaticData, _dataService.RuntimeData, _dataService.SceneSerializedData, _mediator);
            _controllersService.StartControllers();
            _mediator.StartGame();
        }

        public void LoadData() => _dataService.Load();

        public void InitializeGameController(string levelKey)
        {
            _controllersService.CreateGameController(4);
            _controllersService.CreateUiController(_dataService.UIData, _mediator, _mediator.IsJoysticksUse);
            _controllersService.CreateCameraController(Camera.main, _dataService.CameraData, _mediator);
            _controllersService.CreateLogicController(_dataService.LevelData(levelKey), _dataService.StaticData,
                _dataService.RuntimeData, _dataService.SceneSerializedData, _mediator);
            _controllersService.StartControllers();
            _mediator.StartGame();
        }

        public void InitializeInApServicesController() =>
            _inAppServicesController.Initialize(_dataService.AdvertisementsData);

        public void Dispose() => _mediator.OnRestartGameEvent -= RestartLevel;
    }
}