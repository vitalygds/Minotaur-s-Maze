using General;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(UIData), menuName = "ProjectData/" + nameof(UIData))]
    public sealed class UIData : ScriptableObject, IUIData
    {
        [SerializeField] private HudData _hudData;
        public IHudData HudData => _hudData;
        [field: SerializeField] public GameObject HudPrefab  { get; private set; }
        [field: SerializeField] public GameObject MobileControllersPrefab  { get; private set; }
        [field: SerializeField] public GameObject PauseMenuPrefab { get; private set; }
    }
}