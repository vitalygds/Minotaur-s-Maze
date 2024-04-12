using MyGame.General.Data;
using UnityEngine;

namespace MyGame.Data
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "ProjectData/LevelStaticData")]
    public sealed class LevelStaticData : ScriptableObject, ILevelStaticData
    {
        [field: SerializeField] public string LevelKey { get; private set; }
        [field: SerializeField] public Vector2 HeroSpawnPoint { get; private set; }
        [field: SerializeField] public  Vector2 MinotaurSpawnPoint { get; private set; }
        [field: SerializeField] public  Vector2 ArtifactSpawnPoint { get; private set; }

        public void Initialize(string levelKey, Vector2 heroPoint, Vector2 minotaurPoint, Vector2 artifactPoint)
        {
            LevelKey = levelKey;
            HeroSpawnPoint = heroPoint;
            MinotaurSpawnPoint = minotaurPoint;
            ArtifactSpawnPoint = artifactPoint;
        }
    }
}