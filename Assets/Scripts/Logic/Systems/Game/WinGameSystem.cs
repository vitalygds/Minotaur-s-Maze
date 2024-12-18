using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class WinGameSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EndGameTriggerReachedEvent>> _eventFilter = WorldNames.EVENT;

        private readonly IControllersMediator _mediator;

        public WinGameSystem(IControllersMediator mediator) => _mediator = mediator;

        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                _mediator.TryWin();
            }
        }
    }
}