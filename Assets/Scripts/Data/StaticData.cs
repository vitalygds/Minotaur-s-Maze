using General;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(StaticData), menuName = "ProjectData/" + nameof(StaticData))]
    public sealed class StaticData : ScriptableObject, IStaticData
    {
        [SerializeField] private GameObject _heroPrefab;
        [SerializeField] private GameObject _minotaurPrefab;
        [SerializeField] private GameObject _artifactPrefab;

        public GameObject HeroPrefab => _heroPrefab;
        public GameObject MinotaurPrefab => _minotaurPrefab;
        public GameObject ArtifactPrefab => _artifactPrefab;
    }
}