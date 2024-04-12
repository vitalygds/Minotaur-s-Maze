using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace MyGame.Logic.Services.Extensions.Systems
{
    internal sealed class OneFrameSystem<T> : IEcsInitSystem, IEcsRunSystem where T : struct
    {
        private readonly EcsFilter _filter;
        private readonly EcsPool<T> _pool;

        public OneFrameSystem(EcsWorld world)
        {
            _filter = world.Filter<T>().End();
            _pool = world.GetPool<T>();
        }

        public void Init(EcsSystems systems) => RemoveEvent();

        public void Run(EcsSystems systems) => RemoveEvent();

        private void RemoveEvent()
        {
            foreach (var i in _filter)
            {
                _pool.Del(i);
            }
        }
    }
}