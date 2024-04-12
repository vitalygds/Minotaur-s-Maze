using Cysharp.Threading.Tasks;
using MyGame.Core.GameControllers;
using MyGame.Core.Services;
using MyGame.General.Controller;
using MyGame.General.Service;
using MyGame.General.StateMachine.Interfaces;
using UnityEngine;

namespace MyGame.Core.StateMachines.Game
{
    internal sealed class LoadSceneState : ILoadingState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IControllersMediator _mediator;
        private string _levelKey;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingCurtain,
            IGameFactory gameFactory, IControllersMediator mediator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _mediator = mediator;
        }

        public void Enter(string levelKey)
        {
            _levelKey = levelKey;
            _loadingCurtain.Show();
            _sceneLoader.Load(levelKey, OnSceneLoaded);
            _mediator.OnSceneInitializeEvent += ChangeState;
            _mediator.OnGameQuitEvent += QuitGame;
        }

        private void QuitGame() => _sceneLoader.Load(SceneNames.Menu);

        public void Exit() => _mediator.OnSceneInitializeEvent -= ChangeState;

        private void OnSceneLoaded()
        {
            _gameFactory.LoadData();
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
            _gameFactory.InitializeInApServicesController();
#endif
            _gameFactory.InitializeGameController(_levelKey);
        }

        private void ChangeState() => HideCurtainAsync().Forget();

        private async UniTaskVoid HideCurtainAsync()
        {
            await _loadingCurtain.Fade();
            GameController gameController = Object.FindObjectOfType<GameController>();
            _stateMachine.EnterGameLoop(gameController);
        }
    }
}