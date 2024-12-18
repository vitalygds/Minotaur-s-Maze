using General;

namespace Core
{
    internal sealed class Game
    {
        private readonly GameStateMachine _stateMachine;

        public Game(LoadingScreen loadingCurtain, IGameFactory gameFactory, IControllersMediator mediator)
        {
            _stateMachine = new GameStateMachine(new SceneLoader(), loadingCurtain, gameFactory, mediator);
        }

        public void Start(string sceneName) => _stateMachine.Start(sceneName);
    }
}