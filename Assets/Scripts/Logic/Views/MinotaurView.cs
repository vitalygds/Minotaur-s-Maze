using Leopotam.EcsLite;
using MyGame.Logic.Components.Pathfinder;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using Pathfinding;
using UnityEngine;

namespace MyGame.Logic.Views
{
    [SelectionBase]
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class MinotaurView : UnityEcsView
    {
        protected override void OnInitialization(EcsWorld defaultWorld, EcsWorld eventWorld, int entity)
        {
            defaultWorld.GetPool<AnimatorComponent>().Add(entity).Value = GetComponentInChildren<Animator>();
            defaultWorld.GetPool<Rigidbody2DComponent>().Add(entity).Value = GetComponent<Rigidbody2D>();
            defaultWorld.GetPool<Collider2DComponent>().Add(entity).Value = GetComponent<CircleCollider2D>();
            defaultWorld.GetPool<MinotaurComponent>().Add(entity);
            defaultWorld.GetPool<SeekerComponent>().Add(entity).Value = GetComponent<Seeker>();
            defaultWorld.GetPool<AIPathComponent>().Add(entity).Value = GetComponent<AIPath>();
            defaultWorld.GetPool<AIDestinationSetterComponent>().Add(entity).Value = GetComponent<AIDestinationSetter>();
        }
    }
}