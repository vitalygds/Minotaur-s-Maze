namespace General
{
    public interface ITimeService
    {
        float Time { get; }
        float DeltaTime { get; }
        float FixedDeltaTime { get; }

        void PauseGame(bool isPause);
    }
}