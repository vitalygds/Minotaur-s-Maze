using Leopotam.EcsLite;

namespace Logic
{
    public static class EcsPoolsExtensions
    {
        public static ref T Replace<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
            {
                pool.Del(entity);
            }
            pool.Add(entity);
            return ref pool.Get(entity);
        }
    }
}