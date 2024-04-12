using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.Logic.Components.Input;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services;
using UnityEngine;

namespace MyGame.Logic.Systems.Hero
{
    internal sealed class HeroDustOnMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputMovementDirectionEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsPoolInject<InputMovementDirectionEvent> _eventPool = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HeroComponent, DustParticleComponent>> _heroFilter = default;
        private readonly EcsPoolInject<DustParticleComponent> _particlePool = default;
        public void Run(EcsSystems systems)
        {
            foreach (var inputEntity in _eventFilter.Value)
            {
                ref var inputValue = ref _eventPool.Value.Get(inputEntity).Value;
                if (inputValue != Vector2.zero)
                {
                    foreach (var heroEntity in _heroFilter.Value)
                    {
                        ref var particle = ref _particlePool.Value.Get(heroEntity).Value;
                        particle.Play();
                    }
                }
            }
        }
    }
}