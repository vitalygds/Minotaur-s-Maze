using System.Linq;
using Data;
using Logic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
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
