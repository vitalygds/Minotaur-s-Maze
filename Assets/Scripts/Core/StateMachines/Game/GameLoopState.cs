using General;

namespace Core
{
    internal class GameLoopState : ILoadingState<IGameController>
    {
        private readonly GameStateMachine _stateMachine;
        private IGameController _gameController;
        public GameLoopState(GameStateMachine stateMachine) => _stateMachine = stateMachine;
        
        public void Enter(IGameController gameController)
        {
            _gameController = gameController;
        }
        
        public void Exit()
        {
            _gameController = null;
        }
    }
}