using UnityEngine.InputSystem;

namespace General
{
    public interface IInputService
    {
        void AddHeroListener(IHeroInputListener heroListener);
        void RemoveHeroListener(IHeroInputListener heroListener);
        void AddConsoleListener(IConsoleInputListener consoleListener);
        void RemoveConsoleListener(IConsoleInputListener consoleListener);
    }

    public interface IHeroInputListener
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
    }
    public interface IConsoleInputListener
    {
        void OnShow(InputAction.CallbackContext context);
    }
}