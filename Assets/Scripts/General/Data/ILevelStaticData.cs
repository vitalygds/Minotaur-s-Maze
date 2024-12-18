using UnityEngine;

namespace General
{
    public interface ILevelStaticData
    {
        Vector2 MinotaurSpawnPoint { get; }
        Vector2 ArtifactSpawnPoint { get; }
        Vector2 HeroSpawnPoint { get; }
    }
}