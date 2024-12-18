using System;
using Data;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [UnityEditor.CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelStaticData sceneData = (LevelStaticData) target;
            if (GUILayout.Button("Initialize"))
            {
                InitializeSceneData(sceneData);
            }
        }

        public static void InitializeSceneData(LevelStaticData sceneData)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (!sceneData.name.Contains(currentScene))
            {
                Debug.LogError($"The SceneData is not for {currentScene}");
                return;
            }
            var points = FindObjectsOfType<SpawnPoint>();
            Vector2 heroPoint = Vector2.zero;
            Vector2 minotaurPoint = Vector2.zero;
            Vector2 artifactPoint = Vector2.zero;
            foreach (var point in points)
            {
                switch (point.Type)
                {
                    case SpawnType.Hero:
                        heroPoint = point.transform.parent.position;
                        break;
                    case SpawnType.Minotaur:
                        minotaurPoint = point.transform.parent.position;
                        break;
                    case SpawnType.Artifact:
                        artifactPoint = point.transform.parent.position;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            sceneData.Initialize(currentScene, heroPoint, minotaurPoint, artifactPoint);
            
            EditorUtility.SetDirty(sceneData);
            Debug.Log($"SceneData of {currentScene} initialized");
        }
    }
}