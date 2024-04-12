using System.Collections.Generic;
using MyGame.General.Extensions;
using MyGame.General.Service;
using MyGame.General.Services;
using UnityEngine;

namespace MyGame.Core.Services.Pooling
{
    internal sealed class PoolService : IPoolService
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new Dictionary<string, ObjectPool>(16);
        
        public T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab);
                _viewCache[prefab.name] = viewPool;
            }

            return viewPool.Pop(parent).GetOrAddComponent<T>();
        }
        
        public void Destroy(GameObject gameObject)
        {
            if (_viewCache.ContainsKey(gameObject.name))
            {
                _viewCache[gameObject.name].Push(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public void ClearPools() => _viewCache.Clear();
    }
}