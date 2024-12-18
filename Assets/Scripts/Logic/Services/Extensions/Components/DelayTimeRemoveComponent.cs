using Leopotam.EcsLite;

namespace Logic
{
    internal struct DelayTimeRemoveComponent<T> where T : struct
    {
        public float DelayTime;
        public EcsPackedEntity Entity;
    }
}