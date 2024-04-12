using System.Collections.Generic;
using System.Linq;
using MyGame.Data;
using MyGame.General.Data;
using MyGame.General.Service;
using UnityEngine;

namespace MyGame.Core.Services
{
    internal sealed class DataService : IDataService
    {
        private Dictionary<string, LevelStaticData> _levels;
        public ISceneSerializedData SceneSerializedData { get; private set; }
        public IStaticData StaticData { get; private set; }
        public ICameraData CameraData { get; private set; }
        public IUIData UIData { get; private set; }
        public IRuntimeData RuntimeData { get; private set; }
        public IAdvertisementsData AdvertisementsData { get; private set; }
        public ILevelStaticData LevelData(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public void Load()
        {
            _levels = Resources.LoadAll<LevelStaticData>(DataPaths.LevelsDataDirectory)
                .ToDictionary(x => x.LevelKey, x => x);
            SceneSerializedData = Object.FindObjectOfType<OnSceneSerializationData>();
            StaticData = Resources.Load<StaticData>(DataPaths.StaticDataSingle);
            CameraData = Resources.Load<CameraData>(DataPaths.CameraDataSingle);
            UIData = Resources.Load<UIData>(DataPaths.UIDataSingle);
            AdvertisementsData = Resources.Load<AdsData>(DataPaths.AdsDataSingle);
            RuntimeData = Resources.Load<RuntimeData>(DataPaths.RuntimeDataSingle);
        }
    }
}