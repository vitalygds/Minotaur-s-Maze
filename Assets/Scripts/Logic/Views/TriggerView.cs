using System;
using Leopotam.EcsLite;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services.Extensions;
using UnityEngine;

namespace MyGame.Logic.Views
{
    public abstract class TriggerView : UnityEcsView
    {
        [SerializeField] private bool _isActiveOnStart = true;
        [SerializeField] private bool _isReusable;
        protected EcsWorld EventWorld;

        protected override void OnInitialization(EcsWorld defaultWorld, EcsWorld eventWorld, int entity)
        {
            AddTriggerViewComponent(defaultWorld, entity);
            AddColliderComponent(defaultWorld, entity);
            defaultWorld.GetPool<TriggerIsActiveComponent>().Add(entity).Value = _isActiveOnStart;
            EventWorld = eventWorld;
        }

        private void AddColliderComponent(EcsWorld defaultWorld, int entity)
        {
            var colliders = GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    defaultWorld.GetPool<TriggerCollider2DComponent>().Add(entity).Value = collider;
                    break;
                }

                throw new ArgumentException("There are no trigger collider on the view");
            }
        }

        private void AddTriggerViewComponent(EcsWorld defaultWorld, int entity)
        {
            ref var viewComponent = ref defaultWorld.GetPool<TriggerViewComponent>().Add(entity);
            viewComponent.Value = this;
            viewComponent.IsReusable = _isReusable;
        }

        public abstract void Implement();

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            ref var eventEntity = ref EventWorld.SetEvent<OnTriggerEnter2DEvent>();
            eventEntity.Owner = DefaultPackedEntity;
            eventEntity.OtherPosition = col.transform.position;
        }
    }
}