using UnityEngine;

namespace MyGame.General.Data
{
    public interface ILevelStaticData
    {
        Vector2 MinotaurSpawnPoint { get; }
        Vector2 ArtifactSpawnPoint { get; }
        Vector2 HeroSpawnPoint { get; }
    }
}