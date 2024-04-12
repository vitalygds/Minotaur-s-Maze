using System.Linq;
using MyGame.Data;
using MyGame.Logic.Views;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace MyGame.Editor.CustomEditor.StaticData
{
    [UnityEditor.CustomEditor(typeof(OnSceneSerializationData))]
    public class OnSceneSerializationDataEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            OnSceneSerializationData sceneData = (OnSceneSerializationData) target;
            if (GUILayout.Button("Initialize"))
            {
                InitializeSceneData(sceneData);
            }
        }

        private static void InitializeSceneData(OnSceneSerializationData sceneData)
        {
            var onSceneTriggers = FindObjectsOfType<GameObject>().Where(g => g.TryGetComponent<TriggerView>(out _)).ToList();
            
            sceneData.Initialize(onSceneTriggers);
            
            EditorUtility.SetDirty(sceneData);
            Debug.Log($"SceneData serialized of initialized");
        }
    }
}
