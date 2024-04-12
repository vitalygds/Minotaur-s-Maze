using Leopotam.EcsLite;
using MyGame.General.Service.Input;
using MyGame.Logic.Components.Input;
using MyGame.Logic.Services;
using MyGame.Logic.Services.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyGame.Logic.Systems.Game
{
    internal sealed class InputUpdateSystem : IHeroInputListener, IEcsInitSystem, IEcsDestroySystem
    {
        private readonly IInputService _inputService;
        private EcsWorld _eventWorld;

        public InputUpdateSystem(IInputService inputService) => _inputService = inputService;

        public void Init(EcsSystems systems)
        {
            _inputService.AddHeroListener(this);
            _eventWorld = systems.GetWorld(WorldNames.EVENT);
        }

        public void OnMovement(InputAction.CallbackContext context) =>
            _eventWorld.SetEvent<InputMovementDirectionEvent>().Value = context.ReadValue<Vector2>();

        public void OnAction(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _eventWorld.SetEvent<InputActionEvent>();
            }
        }

        public void Destroy(EcsSystems systems) => _inputService.RemoveHeroListener(this);
    }
}