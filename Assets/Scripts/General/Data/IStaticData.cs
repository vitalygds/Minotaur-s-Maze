using UnityEngine;

namespace MyGame.General.Data
{
    public interface IStaticData
    {
        GameObject HeroPrefab { get; }
        GameObject MinotaurPrefab { get; }
        GameObject ArtifactPrefab { get; }
    }
}