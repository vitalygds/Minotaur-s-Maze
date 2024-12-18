using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class EndGameTriggerView : TriggerView
    {
        public override void Implement() => EventWorld.SetEvent<EndGameTriggerReachedEvent>();
    }
}