using UnityEngine;

namespace MyGame.Data
{
    [CreateAssetMenu(fileName = "DungeonData", menuName = "ProjectData/DungeonData")]
    public sealed class DungeonData : ScriptableObject
    {
        [Range(10, 1000)] public int Wight;
        [Range(10, 1000)] public int Height;
    }
}