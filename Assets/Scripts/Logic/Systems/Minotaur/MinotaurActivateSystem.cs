using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services;
using MyGame.Logic.Services.Extensions;

namespace MyGame.Logic.Systems.Minotaur
{
    internal sealed class MinotaurActivateSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HarpPickedUpEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<MinotaurComponent, GameObjectComponent, TransformComponent, DisabledComponent>>
            _minotaurFilter = default;

        private readonly EcsPoolInject<GameObjectComponent> _gameObjectPool = default;
        private readonly EcsPoolInject<DisabledComponent> _disabledPool = default;

        public void Run(EcsSystems systems)
        {
            foreach (var _ in _eventFilter.Value)
            {
                foreach (var minotaurEntity in _minotaurFilter.Value)
                {
                    ref var minotaurView = ref _gameObjectPool.Value.Get(minotaurEntity).Value;
                    minotaurView.SetActive(true);
                    _disabledPool.Value.Del(minotaurEntity);
                    _eventWorld.Value.SetEvent<MinotaurActivateEvent>();
                }
            }
        }
    }
}