using UnityEngine;

namespace General
{
    public interface IStaticData
    {
        GameObject HeroPrefab { get; }
        GameObject MinotaurPrefab { get; }
        GameObject ArtifactPrefab { get; }
    }
}