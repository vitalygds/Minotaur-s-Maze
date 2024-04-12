using Leopotam.EcsLite;
using MyGame.General.Service;
using MyGame.Logic.Services.Extensions.Components;

namespace MyGame.Logic.Services.Extensions.Systems
{
    internal sealed class DelayTimeAddSystem<T> : IEcsInitSystem, IEcsRunSystem where T : struct
    {
        private readonly ITimeService _timeService;
        private readonly EcsWorld _world;
        private readonly EcsFilter _filter;
        private readonly EcsPool<DelayTimeAddComponent<T>> _timePool;
        private readonly EcsPool<T> _componentPool;

        public DelayTimeAddSystem(ITimeService timeService, EcsWorld world)
        {
            _timeService = timeService;
            _world = world;
            _filter = world.Filter<DelayTimeAddComponent<T>>().End();
            _timePool = world.GetPool<DelayTimeAddComponent<T>>();
            _componentPool = world.GetPool<T>();
        }

        public void Init(EcsSystems systems) => CheckTimer();

        public void Run(EcsSystems systems) => CheckTimer();

        private void CheckTimer()
        {
            foreach (var i in _filter)
            {
                ref var timeEntity = ref _timePool.Get(i);
                if ((timeEntity.DelayTime > _timeService.Time)) continue;
                
                if (timeEntity.Entity.Unpack(_world, out int entity))
                {
                    _componentPool.Replace(entity);
                }
                _world.DelEntity(i);
            }
        }
    }
}