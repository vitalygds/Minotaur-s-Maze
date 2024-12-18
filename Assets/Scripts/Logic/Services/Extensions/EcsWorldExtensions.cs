using General;
using Leopotam.EcsLite;

namespace Logic
{
    public static class EcsWorldExtensions
    {
        private const int c_secondsToMillisecondsMultiplier = 1000;

        public static ref T SetEvent<T>(this EcsWorld world) where T : struct =>
            ref world.GetPool<T>().Add(world.NewEntity());

        public static void AddComponentWithTime<T>(this EcsWorld world, ITimeService timeService, float lifeTime,
            int entity) where T : struct
        {
            var timeEntity = world.NewEntity();
            ref var delayComponent = ref world.GetPool<DelayTimeAddComponent<T>>().Add(timeEntity);
            delayComponent.Entity = world.PackEntity(entity);
            delayComponent.DelayTime = timeService.Time + lifeTime;
        }

        public static void RemoveComponentWithTime<T>(this EcsWorld world, ITimeService timeService, float lifeTime,
            int entity) where T : struct
        {
            var timeEntity = world.NewEntity();
            ref var delayComponent = ref world.GetPool<DelayTimeRemoveComponent<T>>().Add(timeEntity);
            delayComponent.Entity = world.PackEntity(entity);
            delayComponent.DelayTime = timeService.Time + lifeTime;
        }
    }
}