using General;
using Leopotam.EcsLite;

namespace Logic
{
    internal sealed class DelayTimeRemoveSystem<T> : IEcsInitSystem, IEcsRunSystem where T : struct
    {
        private readonly ITimeService _timeService;
        private readonly EcsWorld _world;
        private readonly EcsFilter _filter;
        private readonly EcsPool<DelayTimeRemoveComponent<T>> _timerPool;
        private readonly EcsPool<T> _componentPool;

        public DelayTimeRemoveSystem(ITimeService timeService, EcsWorld world)
        {
            _timeService = timeService;
            _world = world;
            _filter = world.Filter<DelayTimeRemoveComponent<T>>().End();
            _timerPool = world.GetPool<DelayTimeRemoveComponent<T>>();
            _componentPool = world.GetPool<T>();
        }

        public void Init(EcsSystems systems) => CheckTimer();

        public void Run(EcsSystems systems) => CheckTimer();

        private void CheckTimer()
        {
            foreach (var i in _filter)
            {
                ref var timeEntity = ref _timerPool.Get(i);
                if ((timeEntity.DelayTime > _timeService.Time)) continue;
                
                if (timeEntity.Entity.Unpack(_world, out int entity))
                {
                    if(_componentPool.Has(entity)) 
                        _componentPool.Del(entity);
                }
                _world.DelEntity(i);
            }
        }
    }
}