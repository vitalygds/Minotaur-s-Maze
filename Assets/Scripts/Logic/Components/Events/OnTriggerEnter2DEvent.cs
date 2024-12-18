using Leopotam.EcsLite;
using UnityEngine;

namespace Logic
{
    public struct OnTriggerEnter2DEvent
    {
        public EcsPackedEntity Owner;
        public Vector2 OtherPosition;
    }
}