using Leopotam.EcsLite;
using MyGame.General.Service;
using MyGame.Logic.Services.Extensions.Systems;

namespace MyGame.Logic.Services.Extensions
{
    public static class EcsSystemsExtensions
    {
        public static EcsSystems OneFrameSystem<T>(this EcsSystems systems, string worldName = null)
            where T : struct => systems.Add(new OneFrameSystem<T>(systems.GetWorld(worldName)));

        public static EcsSystems DelayAddComponentSystem<T>(this EcsSystems systems, ITimeService timeService, string worldName = null)
            where T : struct => systems.Add(new DelayTimeAddSystem<T>(timeService, systems.GetWorld(worldName)));
        public static EcsSystems DelayRemoveComponentSystem<T>(this EcsSystems systems, ITimeService timeService, string worldName = null)
            where T : struct => systems.Add(new DelayTimeRemoveSystem<T>(timeService, systems.GetWorld(worldName)));
    }
}