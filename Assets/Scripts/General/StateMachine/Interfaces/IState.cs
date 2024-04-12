namespace MyGame.General.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}