using MyGame.Logic.Components.Events;
using MyGame.Logic.Services.Extensions;
using UnityEngine;

namespace MyGame.Logic.Views
{
    [SelectionBase]
    [RequireComponent(typeof(CircleCollider2D))]
    internal sealed class HarpView : TriggerView
    { 
        public override void Implement() => EventWorld.SetEvent<HarpPickedUpEvent>();
    }
}