using MyGame.General.Data;

namespace MyGame.General.Service
{
    public interface IDataService
    {
        void Load();
        ISceneSerializedData SceneSerializedData { get; }
        IStaticData StaticData { get; }
        IAdvertisementsData AdvertisementsData { get; }
        ILevelStaticData LevelData(string sceneKey);
        IUIData UIData { get; }
        ICameraData CameraData { get; }
        IRuntimeData RuntimeData { get; }
    }
}