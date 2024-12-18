using System.Collections.Generic;
using General;
using UnityEngine.InputSystem;

namespace Core
{
    internal sealed class InputService : IInputService, InputControls.IHeroActions, InputControls.IConsoleActions
    {
        private readonly InputControls _controls;
        private readonly List<IHeroInputListener> _heroInputListeners;
        private readonly List<IConsoleInputListener> _consoleInputListeners;

        public InputService()
        {
            _controls = new InputControls();
            _heroInputListeners = new List<IHeroInputListener>();
            _consoleInputListeners = new List<IConsoleInputListener>();
            _controls.Hero.SetCallbacks(this);
            _controls.Hero.Enable();
            _controls.Console.SetCallbacks(this);
            _controls.Console.Enable();
        }

        public void AddHeroListener(IHeroInputListener heroListener) => _heroInputListeners.Add(heroListener);

        public void RemoveHeroListener(IHeroInputListener heroListener)
        {
            if (_heroInputListeners.Contains(heroListener))
                _heroInputListeners.Remove(heroListener);
        }

        public void AddConsoleListener(IConsoleInputListener consoleListener) => _consoleInputListeners.Add(consoleListener);

        public void RemoveConsoleListener(IConsoleInputListener consoleListener)
        {
            if (_consoleInputListeners.Contains(consoleListener))
                _consoleInputListeners.Remove(consoleListener);
        }


        void InputControls.IHeroActions.OnMovement(InputAction.CallbackContext context)
        {
            foreach (var heroListener in _heroInputListeners)
            {
                heroListener.OnMovement(context);
            }
        }

        void InputControls.IHeroActions.OnAction(InputAction.CallbackContext context)
        {
            foreach (var heroListener in _heroInputListeners)
            {
                heroListener.OnAction(context);
            }
        }

        void InputControls.IConsoleActions.OnShow(InputAction.CallbackContext context)
        {
            foreach (var consoleListener in _consoleInputListeners)
            {
                consoleListener.OnShow(context);
            }
        }
    }
}