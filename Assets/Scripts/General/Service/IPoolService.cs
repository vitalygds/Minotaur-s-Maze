using UnityEngine;

namespace General
{
    public interface IPoolService
    {
        T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component;
        void Destroy(GameObject gameObject);
        void ClearPools();
    }
}
