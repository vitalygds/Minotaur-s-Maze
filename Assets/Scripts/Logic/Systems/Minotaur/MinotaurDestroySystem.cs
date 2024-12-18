﻿using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class MinotaurDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EndGameTriggerReachedEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<MinotaurComponent, TargetComponent>> _minotaurFilter = default;
        private readonly EcsPoolInject<OnDestroyComponent> _destroyPool = default;
        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                foreach (var minotaurEntity in _minotaurFilter.Value)
                {
                    _destroyPool.Value.Replace(minotaurEntity);
                }
            }
        }
    }
}