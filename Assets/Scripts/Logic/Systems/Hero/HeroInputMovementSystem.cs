using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    public sealed class HeroInputMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputMovementDirectionEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsPoolInject<InputMovementDirectionEvent> _eventPool = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HeroComponent>> _heroFilter = default;
        private readonly EcsPoolInject<InputMovementDirectionComponent> _inputPool = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                ref var inputValue = ref _eventPool.Value.Get(eventEntity).Value;
                foreach (var heroEntity in _heroFilter.Value)
                {
                    _inputPool.Value.Replace(heroEntity).Value = inputValue;
                }
            }
        }
    }
}