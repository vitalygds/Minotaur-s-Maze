using System;
using MyGame.Data.Level;
using UnityEditor;
using UnityEngine;

namespace MyGame.Editor.CustomEditor
{
    [UnityEditor.CustomEditor(typeof(SpawnPoint))]
    public class SpawnMarkersEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(SpawnPoint spawnPoint, GizmoType gizmo)
        {
            Gizmos.color = spawnPoint.Type switch
            {
                SpawnType.Hero     => new Color(0.0f, 1f, 1f, 0.5f),
                SpawnType.Minotaur => new Color(1f, 0.0f, 0.0f, 0.5f),
                SpawnType.Artifact => new Color(1f, 1.0f, 0.0f, 0.5f),
                _                  => throw new ArgumentOutOfRangeException()
            };
            
            Gizmos.DrawSphere(spawnPoint.transform.position, 0.5f);
        }
    }
}
