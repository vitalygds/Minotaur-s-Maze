using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services;
using UnityEngine;

namespace MyGame.Logic.Systems.Hero
{
    internal sealed class HeroOnTriggerEnterSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<OnTriggerEnter2DEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsPoolInject<OnTriggerEnter2DEvent> _eventPool = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HeroComponent, TransformComponent>> _heroFilter = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<TriggerViewComponent> _triggerPool = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                ref var eventComponent = ref _eventPool.Value.Get(eventEntity);
                foreach (var heroEntity in _heroFilter.Value)
                {
                    ref var heroTransform = ref _transformPool.Value.Get(heroEntity).Value;
                    Vector2 heroPosition = heroTransform.position;
                    
                    if (eventComponent.OtherPosition == heroPosition)
                    {
                        TryImplementTrigger(eventComponent);
                    }
                }
            }
        }

        private void TryImplementTrigger(OnTriggerEnter2DEvent eventComponent)
        {
            if (eventComponent.Owner.Unpack(_world.Value, out var triggerEntity))
            {
                if (_triggerPool.Value.Has(triggerEntity))
                {
                    ref var triggerView = ref _triggerPool.Value.Get(triggerEntity);
                    triggerView.Value.Implement();
                    if (!triggerView.IsReusable)
                    {
                        _world.Value.GetPool<OnDestroyComponent>().Add(triggerEntity);
                    }
                }
            }
        }
    }
}