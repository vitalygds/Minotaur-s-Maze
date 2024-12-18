namespace General
{
    public interface IGameController
    {
        void Initialize(int controllersCapacity);
        void AddController<T>(IController controller) where T : IController;
        void Enable();
        void Disable();
        void DestroyControllers();
        void DestroyLogic();
    }
}