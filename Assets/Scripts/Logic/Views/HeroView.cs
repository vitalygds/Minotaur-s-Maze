using Leopotam.EcsLite;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using UnityEngine;

namespace MyGame.Logic.Views
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class HeroView : UnityEcsView
    {
        [SerializeField] private ParticleSystem _dustParticleSystem;
        [SerializeField] private ParticleSystem _dashParticleSystem;
        [SerializeField] private Material _material;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        protected override void OnInitialization(EcsWorld defaultWorld, EcsWorld eventWorld, int entity)
        {
            defaultWorld.GetPool<AnimatorComponent>().Add(entity).Value = GetComponentInChildren<Animator>();
            defaultWorld.GetPool<Rigidbody2DComponent>().Add(entity).Value = GetComponent<Rigidbody2D>();
            defaultWorld.GetPool<Collider2DComponent>().Add(entity).Value = GetComponent<CircleCollider2D>();
            defaultWorld.GetPool<HeroComponent>().Add(entity);
            defaultWorld.GetPool<DustParticleComponent>().Add(entity).Value = _dustParticleSystem;
            defaultWorld.GetPool<DashParticleComponent>().Add(entity).Value = _dashParticleSystem;
            
            var materialEntity = defaultWorld.NewEntity();
            defaultWorld.GetPool<SpriteRendererComponent>().Add(materialEntity).Value = _spriteRenderer;
            defaultWorld.GetPool<MaterialComponent>().Add(materialEntity).Value = _material;
            defaultWorld.GetPool<MaterialSpriteUpdateComponent>().Add(materialEntity);
        }
    }
}