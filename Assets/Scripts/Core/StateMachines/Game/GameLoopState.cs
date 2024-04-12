using MyGame.General.Controller;
using MyGame.General.StateMachine.Interfaces;

namespace MyGame.Core.StateMachines.Game
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