using UnityEngine;

namespace General
{
    public interface IUIData
    {
        IHudData HudData { get; }
        GameObject HudPrefab { get; }
        GameObject MobileControllersPrefab { get; }
        GameObject PauseMenuPrefab { get; }
    }
}