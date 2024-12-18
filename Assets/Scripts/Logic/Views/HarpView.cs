using UnityEngine;

namespace Logic
{
    [SelectionBase]
    [RequireComponent(typeof(CircleCollider2D))]
    internal sealed class HarpView : TriggerView
    { 
        public override void Implement() => EventWorld.SetEvent<HarpPickedUpEvent>();
    }
}