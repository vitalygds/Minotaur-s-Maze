using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class HeroActionInputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsFilterInject<Inc<InputActionEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HeroComponent, DashParticleComponent, Rigidbody2DComponent>, Exc<CooldownComponent>> _heroFilter = default;
        private readonly EcsPoolInject<Rigidbody2DComponent> _rigidbodyPool = default;
        private readonly EcsPoolInject<DashParticleComponent> _dashParticlePool = default;
        private readonly EcsPoolInject<DashComponent> _dashPool = default;
        private readonly EcsPoolInject<CooldownComponent> _cooldownPool = default;
        private readonly ITimeService _timeService;
        private readonly IRuntimeData _runtimeData;

        public HeroActionInputSystem(ITimeService timeService, IRuntimeData runtimeData)
        {
            _timeService = timeService;
            _runtimeData = runtimeData;
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _eventFilter.Value)
            {
                foreach (var heroEntity in _heroFilter.Value)
                {
                    ref var rigidbody = ref _rigidbodyPool.Value.Get(heroEntity).Value;
                    var bodyVector = rigidbody.velocity;
                    bodyVector *= _runtimeData.HeroDashSpeedMultiplier;
                    rigidbody.velocity = bodyVector;
                    ref var dashParticle = ref _dashParticlePool.Value.Get(heroEntity).Value;
                    dashParticle.Play();
                    _cooldownPool.Value.Add(heroEntity);
                    _dashPool.Value.Add(heroEntity);
                    _world.Value.RemoveComponentWithTime<DashComponent>(_timeService, _runtimeData.HeroDashTime, heroEntity);
                    _world.Value.RemoveComponentWithTime<CooldownComponent>(_timeService, 
                        _runtimeData.HeroDashCoolDownTime + _runtimeData.HeroDashTime, heroEntity);
                }
            }
        }
    }
}