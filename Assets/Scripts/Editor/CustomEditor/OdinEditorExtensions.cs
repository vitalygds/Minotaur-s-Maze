using System.IO;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace MyGame.Editor.CustomEditor
{
    public class OdinEditorExtensions
    {
        public static void DrawCreateNewButton<T>(string path) where T : ScriptableObject
        {
            if (SirenixEditorGUI.ToolbarButton("Create new data"))
            {
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), GenerateName(path, "New"));
                AssetDatabase.SaveAssets();
            }
        }

        public static void DrawForceReSaveButton<T>(OdinMenuItem selected)
        {
            if (SirenixEditorGUI.ToolbarButton("Save Project"))
            {
                EditorApplication.ExecuteMenuItem("File/Save Project");
            }
        }

        public static void DrawDeleteButton<T>(OdinMenuTree menuTree) where T : ScriptableObject
        {
            if (SirenixEditorGUI.ToolbarButton("Delete current"))
            {
                T asset = menuTree.Selection.SelectedValue as T;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }

        private static string GenerateName(string path, string name)
        {
            int saveNumber = 1;
            while (File.Exists(path + "/" + name + saveNumber + ".asset"))
            {
                saveNumber++;
            }

            return path + "/" + name + saveNumber + ".asset";
        }
    }
}