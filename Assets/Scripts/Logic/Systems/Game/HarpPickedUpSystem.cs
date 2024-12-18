using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class HarpPickedUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HarpPickedUpEvent>> _eventFilter = WorldNames.EVENT;
        private readonly IControllersMediator _mediator;

        public HarpPickedUpSystem(IControllersMediator mediator) => _mediator = mediator;

        public void Run(EcsSystems systems)
        {
            foreach (int _ in _eventFilter.Value)
            {
                _mediator.SetArtifactPicked();
                MusicController.Instance.OnObjectiveComplete();
            }
        }
    }
}