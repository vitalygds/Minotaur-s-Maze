﻿using Leopotam.EcsLite;
using UnityEngine;

namespace Logic
{
    [SelectionBase]
    public abstract class UnityEcsView : MonoBehaviour, IEcsView
    {
        public Transform Transform => transform;
        public GameObject GameObject => gameObject;
        protected EcsPackedEntity DefaultPackedEntity;

        void IEcsView.InitializeView(EcsWorld defaultWorld, EcsWorld eventWorld, int entity)
        {
            defaultWorld.GetPool<TransformComponent>().Add(entity).Value = transform;
            defaultWorld.GetPool<GameObjectComponent>().Add(entity).Value = gameObject;
            defaultWorld.GetPool<ViewComponent>().Add(entity).Value = this;
            DefaultPackedEntity = defaultWorld.PackEntity(entity);
            OnInitialization(defaultWorld, eventWorld, entity);
        }
        protected virtual void OnInitialization(EcsWorld defaultWorld, EcsWorld eventWorld, int entity){}
    }
}