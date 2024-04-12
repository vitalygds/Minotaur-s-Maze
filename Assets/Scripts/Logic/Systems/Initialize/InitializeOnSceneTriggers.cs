using Leopotam.EcsLite;
using MyGame.General.Data;
using MyGame.Logic.Services;
using MyGame.Logic.Services.Views;
using MyGame.Logic.Views;
using UnityEngine;

namespace MyGame.Logic.Systems.Initialize
{
    internal class InitializeOnSceneTriggers : IEcsInitSystem
    {
        private readonly ISceneSerializedData _sceneSerializedData;

        public InitializeOnSceneTriggers(ISceneSerializedData sceneSerializedData) => _sceneSerializedData = sceneSerializedData;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var eventWorld = systems.GetWorld(WorldNames.EVENT);
            
            foreach (var trigger in _sceneSerializedData.OnSceneTriggers)
            {
                trigger.SetActive(true);
                if (trigger.TryGetComponent<IEcsView>(out var triggerView))
                    triggerView.InitializeView(world, eventWorld, world.NewEntity());
            }
        }
    }
}