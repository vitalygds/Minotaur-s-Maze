using System.Collections.Generic;
using UnityEngine;

namespace MyGame.General.Service
{
    public interface IPoolService
    {
        T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component;
        void Destroy(GameObject gameObject);
        void ClearPools();
    }
}
