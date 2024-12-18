using General;
using UI;
using UnityEngine;

namespace Core
{
    internal sealed class UIController : IUIController
    {
        public UIController(IUIData data, IControllersMediator mediator, bool isUseJoysticks)
        {
            var hud = Object.Instantiate(data.HudPrefab).GetOrAddComponent<GameHUD>();
            var pauseMenu = Object.Instantiate(data.PauseMenuPrefab).GetOrAddComponent<PauseMenu>();
            hud.Construct(mediator, data.HudData, pauseMenu);
            pauseMenu.Construct(mediator);
            if (isUseJoysticks)
            {
                Object.Instantiate(data.MobileControllersPrefab);
            }
        }
    }
}