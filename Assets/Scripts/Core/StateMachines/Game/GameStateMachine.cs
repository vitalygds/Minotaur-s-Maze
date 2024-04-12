using System;
using System.Collections.Generic;
using MyGame.Core.Services;
using MyGame.General.Controller;
using MyGame.General.Service;
using MyGame.General.StateMachine;
using MyGame.General.StateMachine.Interfaces;

namespace MyGame.Core.StateMachines.Game
{
    internal sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingCurtain, IGameFactory gameFactory, IControllersMediator mediator)
        {
            StatesMap = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, loadingCurtain, gameFactory, mediator),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Start(string sceneName) => Enter<BootstrapState, string>(sceneName);
        public void LoadScene(string sceneName) => Enter<LoadSceneState, string>(sceneName);
        public void EnterGameLoop(IGameController gameController) =>
            Enter<GameLoopState, IGameController>(gameController);
    }
}