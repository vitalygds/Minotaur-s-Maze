namespace General
{
    public interface IGameFactory
    {
        void InitializeGameController(string levelKey);
        void LoadData();
        void InitializeInApServicesController();
    }
}