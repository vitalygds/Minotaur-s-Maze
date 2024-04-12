using Leopotam.EcsLite;
using UnityEngine;

namespace MyGame.Logic.Components.Events
{
    public struct OnTriggerEnter2DEvent
    {
        public EcsPackedEntity Owner;
        public Vector2 OtherPosition;
    }
}