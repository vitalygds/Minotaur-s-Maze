using General;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class MediatorEventsSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly EcsWorldInject _eventWorld = WorldNames.EVENT;
        private readonly IControllersMediator _mediator;

        public MediatorEventsSystem(IControllersMediator mediator) => _mediator = mediator;

        public void Init(EcsSystems systems)
        {
            _mediator.OnArtifactPickUpEvent += SendArtifactPicked;
        }

        private void SendArtifactPicked() => _eventWorld.Value.SetEvent<HarpPickedUpEvent>();

        public void Destroy(EcsSystems systems)
        {
            _mediator.OnArtifactPickUpEvent -= SendArtifactPicked;
        }
    }
}