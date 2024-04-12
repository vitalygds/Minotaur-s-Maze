namespace MyGame.General.StateMachine.Interfaces
{
    public interface ILoadingState<TLoad> : IExitableState
    {
        void Enter(TLoad load);
    }
}