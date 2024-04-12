using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace MyGame.Editor.CustomEditor
{
    public class SimpleTreeMenuEditor<T> where T : ScriptableObject
    {
        private readonly string _itemDataPath;
        private readonly OdinMenuEditorWindow _parent;

        public SimpleTreeMenuEditor(string itemDataPath, OdinMenuEditorWindow parent)
        {
            _itemDataPath = itemDataPath;
            _parent = parent;
        }

        public OdinMenuTree BuildMenuTree(string header)
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            tree.Config.DrawSearchToolbar = true;
            tree.Add(header, null);
            tree.AddAllAssetsAtPath(header, _itemDataPath, typeof(T), true);
            return tree;
        }
        
        public void OnBeginDrawEditors(OdinMenuTree menuTree)
        {
            OdinMenuItem selected = menuTree.Selection.FirstOrDefault();
            int toolbarHeight = menuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                    GUILayout.Label(selected.Name);
                OdinEditorExtensions.DrawCreateNewButton<T>(_itemDataPath);
                OdinEditorExtensions.DrawForceReSaveButton<T>(selected);
                OdinEditorExtensions.DrawDeleteButton<T>(menuTree);
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
        protected virtual void OnNewCreated(T design) => _parent.TrySelectMenuItemWithObject(design);
        
    }
}