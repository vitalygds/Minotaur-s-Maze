using General;

namespace Core
{
    internal sealed class TimeService : ITimeService
    {
        public float Time => UnityEngine.Time.time;
        public float DeltaTime => UnityEngine.Time.deltaTime;
        public float FixedDeltaTime => UnityEngine.Time.fixedDeltaTime;

        public void PauseGame(bool isPause)
        {
            UnityEngine.Time.timeScale = isPause ? 0 : 1;
        }
    }
}