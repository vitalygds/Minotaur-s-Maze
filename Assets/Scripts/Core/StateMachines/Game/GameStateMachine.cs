using System;
using System.Collections.Generic;
using General;

namespace Core
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