using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.Logic.Animators;
using MyGame.Logic.Components.Input;
using MyGame.Logic.Components.Skills;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using UnityEngine;

namespace MyGame.Logic.Systems.Hero
{
    internal sealed class HeroAnimateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorComponent, HeroComponent, InputMovementDirectionComponent>, 
                Exc<DashComponent>> _heroFilter = default;
        private readonly EcsPoolInject<AnimatorComponent> _animatorPool = default;
        private readonly EcsPoolInject<InputMovementDirectionComponent> _inputPool = default;
        public void Run(EcsSystems systems)
        {
            foreach (var heroEntity in _heroFilter.Value)
            {
                ref var heroAnimator = ref _animatorPool.Value.Get(heroEntity).Value;
                ref var inputValue = ref _inputPool.Value.Get(heroEntity).Value;
                heroAnimator.SetBool(AnimatorVariables.IsWalking, inputValue != Vector2.zero);
                heroAnimator.SetFloat(AnimatorVariables.HorizontalInput, inputValue.x);
                heroAnimator.SetFloat(AnimatorVariables.VerticalInput, inputValue.y);
            }
        }
    }
}