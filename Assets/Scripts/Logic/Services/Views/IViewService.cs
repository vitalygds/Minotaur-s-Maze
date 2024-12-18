using UnityEngine;

namespace Logic
{
    internal interface IViewService
    {
        T CreateView<T>(int entity, GameObject prefab, Transform parent) where T : Component, IEcsView;

        void DestroyView(int entity);
    }
}