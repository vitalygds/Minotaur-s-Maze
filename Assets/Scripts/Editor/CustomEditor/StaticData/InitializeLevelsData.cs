using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class InitializeLevelsData
    {
        [MenuItem("MyGame/InitializeLevelData")]
        public static void InitializeSceneData()
        {
            IEnumerable<Object> staticData = Resources.LoadAll(DataPaths.LevelsDataDirectory)
                .Where(d => d is LevelStaticData);
            foreach (Object data in staticData)
            {
                LevelStaticDataEditor.InitializeSceneData((LevelStaticData) data);
            }
        }
    }
}