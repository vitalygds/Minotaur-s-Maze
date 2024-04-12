using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using MyGame.General.Controller;
using MyGame.Logic.Components.Events;
using MyGame.Logic.Services;

namespace MyGame.Logic.Systems.Game
{
    internal sealed class HarpPickedUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HarpPickedUpEvent>> _eventFilter = WorldNames.EVENT;
        private readonly IControllersMediator _mediator;

        public HarpPickedUpSystem(IControllersMediator mediator) => _mediator = mediator;

        public void Run(EcsSystems systems)
        {
            foreach (var _ in _eventFilter.Value)
            {
                _mediator.SetArtifactPicked();
                MusicController.Instance.OnObjectiveComplete();
            }
        }
    }
}