using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class HeroMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Rigidbody2DComponent, HeroComponent, InputMovementDirectionComponent>, 
                Exc<DashComponent>> _heroFilter = default;
        private readonly EcsPoolInject<Rigidbody2DComponent> _rigidbodyPool = default;
        private readonly EcsPoolInject<InputMovementDirectionComponent> _inputPool = default;

        private readonly ITimeService _timeService;
        private readonly IRuntimeData _runtimeData;

        public HeroMovementSystem(ITimeService timeService, IRuntimeData runtimeData)
        {
            _timeService = timeService;
            _runtimeData = runtimeData;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var heroEntity in _heroFilter.Value)
            {
                ref var heroBody = ref _rigidbodyPool.Value.Get(heroEntity).Value;
                ref var inputValue = ref _inputPool.Value.Get(heroEntity).Value;
                heroBody.velocity = inputValue * (_timeService.FixedDeltaTime * _runtimeData.HeroMovementSpeed);
            }
        }
    }
}