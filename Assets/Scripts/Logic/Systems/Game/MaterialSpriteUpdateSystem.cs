using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Logic
{
    internal sealed class MaterialSpriteUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<InputMovementDirectionEvent>> _eventFilter = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<MaterialComponent, SpriteRendererComponent, MaterialSpriteUpdateComponent>> _filter = default;
        private readonly EcsPoolInject<MaterialComponent> _materialPool = default;
        private readonly EcsPoolInject<SpriteRendererComponent> _rendererPool = default;
        public void Run(EcsSystems systems)
        {
            foreach (var eventEntity in _eventFilter.Value)
            {
                foreach (var entity in _filter.Value)
                {
                    ref var material = ref _materialPool.Value.Get(entity).Value;
                    ref var renderer = ref _rendererPool.Value.Get(entity).Value;
                    material.mainTexture = renderer.sprite.texture;
                }
            }
        }
    }
}