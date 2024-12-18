using General;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(HudData), menuName = "ProjectData/" + nameof(HudData))]
    internal sealed class HudData : ScriptableObject, IHudData
    {
        [field: SerializeField] public string WinMessage { get; private set; }
        [field: SerializeField] public string StartMessage { get; private set; }
        [field: SerializeField] public string OnArtifactPickMessage { get; private set; }
        [field: SerializeField] public int MessageShowDelayTimeMillisecond { get; private set;}
        [field: SerializeField] public int AdsButtonShowDelayTimeMillisecond { get; private set;}
    }
}