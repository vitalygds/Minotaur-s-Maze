namespace General
{
    public interface ILoadingState<TLoad> : IExitableState
    {
        void Enter(TLoad load);
    }
}