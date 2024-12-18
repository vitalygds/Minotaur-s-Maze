namespace General
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}