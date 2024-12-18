using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    public sealed class MinotaurDestinationSetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MinotaurActivateEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<MinotaurComponent, TransformComponent, AIDestinationSetterComponent>, 
            Exc<TargetComponent>> _minotaurFilter = default;
        private readonly EcsFilterInject<Inc<HeroComponent, TransformComponent>> _heroFilter = default;
        private readonly EcsPoolInject<AIDestinationSetterComponent> _aiDestinationPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<TargetComponent> _targetPool = default;

        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                foreach (var minotaurEntity in _minotaurFilter.Value)
                {
                    ref var minotaurAi = ref _aiDestinationPool.Value.Get(minotaurEntity).Value;
                    foreach (var heroEntity in _heroFilter.Value)
                    {
                        ref var heroTransform = ref _transformPool.Value.Get(heroEntity).Value;
                        minotaurAi.target = heroTransform;
                        minotaurAi.enabled = true;
                        _targetPool.Value.Replace(minotaurEntity).Value = heroTransform;
                    }
                }
            }
        }
    }
}