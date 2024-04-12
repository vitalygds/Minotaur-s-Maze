namespace MyGame.General.Service
{
    public interface ITimeService
    {
        float Time { get; }
        float DeltaTime { get; }
        float FixedDeltaTime { get; }

        void PauseGame(bool isPause);
    }
}