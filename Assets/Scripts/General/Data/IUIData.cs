using UnityEngine;

namespace MyGame.General.Data
{
    public interface IUIData
    {
        IHudData HudData { get; }
        GameObject HudPrefab { get; }
        GameObject MobileControllersPrefab { get; }
        GameObject PauseMenuPrefab { get; }
    }
}