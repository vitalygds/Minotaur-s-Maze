using Leopotam.EcsLite;
using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(Collider2D))]
    internal sealed class SpikesTriggerView : TriggerView
    {
        protected override void OnInitialization(EcsWorld defaultWorld, EcsWorld eventWorld, int entity)
        {
            base.OnInitialization(defaultWorld, eventWorld, entity);
            defaultWorld.GetPool<AnimatorComponent>().Add(entity).Value = GetComponentInChildren<Animator>();
            defaultWorld.GetPool<SpikesTriggerViewComponent>().Add(entity);
        }

        public override void Implement()
        {
            Debug.Log("Damage");
        }
    }
}