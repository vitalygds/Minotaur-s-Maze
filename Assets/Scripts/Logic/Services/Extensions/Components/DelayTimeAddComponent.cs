using Leopotam.EcsLite;

namespace Logic
{
    internal struct DelayTimeAddComponent<T> where T : struct
    {
        public float DelayTime;
        public EcsPackedEntity Entity;
    }
}