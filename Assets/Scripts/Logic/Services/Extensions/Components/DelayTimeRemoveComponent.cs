using Leopotam.EcsLite;

namespace MyGame.Logic.Services.Extensions.Components
{
    internal struct DelayTimeRemoveComponent<T> where T : struct
    {
        public float DelayTime;
        public EcsPackedEntity Entity;
    }
}