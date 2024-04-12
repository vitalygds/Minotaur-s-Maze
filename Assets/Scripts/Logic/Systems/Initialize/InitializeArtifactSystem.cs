using Leopotam.EcsLite;
using MyGame.General.Data;
using MyGame.Logic.Services.Views;
using MyGame.Logic.Views;

namespace MyGame.Logic.Systems.Initialize
{
    internal sealed class InitializeArtifactSystem : IEcsInitSystem
    {
        private readonly IViewService _viewService;
        private readonly ILevelStaticData _levelData;
        private readonly IStaticData _staticData;

        public InitializeArtifactSystem(IViewService viewService, ILevelStaticData levelData, IStaticData staticData)
        {
            _viewService = viewService;
            _levelData = levelData;
            _staticData = staticData;
        }

        public void Init(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var artifactEntity = world.NewEntity();
            var artifactView = _viewService.CreateView<HarpView>(artifactEntity, _staticData.ArtifactPrefab, null);
            artifactView.Transform.position = _levelData.ArtifactSpawnPoint;
        }
    }
}