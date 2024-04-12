using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.General.Controller;
using MyGame.Logic.Components;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Components.Unique;
using MyGame.Logic.Components.Unity;
using MyGame.Logic.Services;
using MyGame.Logic.Services.Extensions;

namespace MyGame.Logic.Systems.Game
{
    internal sealed class GameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<HeroComponent, ViewComponent>> _heroFilter = default;
        private readonly EcsPoolInject<OnDestroyComponent> _destroyPool = default;
        private readonly IControllersMediator _mediator;

        public GameOverSystem(IControllersMediator mediator) => _mediator = mediator;

        public void Run(EcsSystems systems)
        {
            foreach (var entity in _eventFilter.Value)
            {
                foreach (var heroEntity in _heroFilter.Value)
                {
                    _destroyPool.Value.Replace(heroEntity);
                }
                _mediator.GameOver();
            }
        }
    }
}