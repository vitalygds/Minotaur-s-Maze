using MyGame.Logic.Components.Events;
using MyGame.Logic.Services.Extensions;
using UnityEngine;

namespace MyGame.Logic.Views
{
    [RequireComponent(typeof(BoxCollider2D))]
    internal sealed class EndGameTriggerView : TriggerView
    {
        public override void Implement() => EventWorld.SetEvent<EndGameTriggerReachedEvent>();
    }
}