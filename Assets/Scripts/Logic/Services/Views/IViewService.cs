using UnityEngine;

namespace MyGame.Logic.Services.Views
{
    internal interface IViewService
    {
        T CreateView<T>(int entity, GameObject prefab, Transform parent) where T : Component, IEcsView;

        void DestroyView(int entity);
    }
}