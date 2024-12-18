using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    public class SpikesAnimateSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<SpikesTriggerViewComponent, AnimatorComponent, TriggerIsActiveComponent>,
            Exc<CooldownComponent>> _spikesFilter = default;
        private readonly EcsPoolInject<AnimatorComponent> _animatorPool = default;
        private readonly EcsPoolInject<TriggerIsActiveComponent> _activePool = default;
        private readonly EcsPoolInject<CooldownComponent> _cooldownPool = default;
        private readonly ITimeService _timeService;
        private readonly IRuntimeData _runtimeData;

        public SpikesAnimateSystem(ITimeService timeService, IRuntimeData runtimeData)
        {
            _timeService = timeService;
            _runtimeData = runtimeData;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var spikeEntity in _spikesFilter.Value)
            {
                ref var isActive = ref _activePool.Value.Get(spikeEntity).Value;
                isActive = !isActive;
                ref var animator = ref _animatorPool.Value.Get(spikeEntity).Value;
                animator.Play(isActive ? SpikesAnimatorVariables.Hide : SpikesAnimatorVariables.Show);
                _cooldownPool.Value.Add(spikeEntity);
                _world.Value.RemoveComponentWithTime<CooldownComponent>(_timeService, 3f, spikeEntity);
            }
        }
    }
}