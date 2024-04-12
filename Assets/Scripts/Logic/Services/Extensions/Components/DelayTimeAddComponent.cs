using Leopotam.EcsLite;

namespace MyGame.Logic.Services.Extensions.Components
{
    internal struct DelayTimeAddComponent<T> where T : struct
    {
        public float DelayTime;
        public EcsPackedEntity Entity;
    }
}