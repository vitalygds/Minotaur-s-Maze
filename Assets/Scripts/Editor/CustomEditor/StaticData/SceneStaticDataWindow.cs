using Data;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SceneStaticDataWindow : OdinMenuEditorWindow
    {
        private readonly SimpleTreeMenuEditor<LevelStaticData> _editor;

        public SceneStaticDataWindow() =>
            _editor = new SimpleTreeMenuEditor<LevelStaticData>("Assets/Resources/" + DataPaths.LevelsDataDirectory, this);

        [MenuItem("MyGame/LevelsDataEditor")]
        private static void ShowEditor()
        {
            var window = GetWindow<SceneStaticDataWindow>();
            window.titleContent = new GUIContent("Levels Data Editor");
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree() => _editor.BuildMenuTree("Scenes");
        protected override void OnBeginDrawEditors() => _editor.OnBeginDrawEditors(MenuTree);
    }
}