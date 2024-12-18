using General;
using Leopotam.EcsLite;

namespace Logic
{
    internal class InitializeOnSceneTriggers : IEcsInitSystem
    {
        private readonly ISceneSerializedData _sceneSerializedData;

        public InitializeOnSceneTriggers(ISceneSerializedData sceneSerializedData) => _sceneSerializedData = sceneSerializedData;

        public void Init(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            EcsWorld eventWorld = systems.GetWorld(WorldNames.EVENT);
            
            foreach (var trigger in _sceneSerializedData.OnSceneTriggers)
            {
                trigger.SetActive(true);
                if (trigger.TryGetComponent<IEcsView>(out var triggerView))
                    triggerView.InitializeView(world, eventWorld, world.NewEntity());
            }
        }
    }
}