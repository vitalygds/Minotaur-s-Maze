using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Logic
{
    internal sealed class MinotaurAnimateSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = WorldNames.EVENT;
        private readonly EcsFilterInject<Inc<MinotaurComponent, TransformComponent, AnimatorComponent, AIPathComponent,
            AIDestinationSetterComponent, TargetComponent>> _minotaurFilter = default;
        private readonly EcsPoolInject<AnimatorComponent> _animatorPool = default;
        private readonly EcsPoolInject<AIPathComponent> _aiPathPool = default;
        private readonly EcsPoolInject<AIDestinationSetterComponent> _destinationPool = default;
        private readonly EcsPoolInject<TransformComponent> _transformPool = default;
        private readonly EcsPoolInject<TargetComponent> _targetPool = default;
        
        public void Run(EcsSystems systems)
        {
            foreach (var minotaurEntity in _minotaurFilter.Value)
            {
                ref var minotaurAnimator = ref _animatorPool.Value.Get(minotaurEntity).Value;
                ref var minotaurTransform = ref _transformPool.Value.Get(minotaurEntity).Value;
                ref var minotaurDestination = ref _destinationPool.Value.Get(minotaurEntity).Value;
                ref var aiPath = ref _aiPathPool.Value.Get(minotaurEntity).Value;
                var distance = Vector2.Distance(minotaurTransform.position, minotaurDestination.target.position);
                float animationSpeed;
                if (distance < 7f)
                {
                    aiPath.maxSpeed = 10f;
                    animationSpeed = 3f;
                }
                else
                {
                    aiPath.maxSpeed = 5f;
                    animationSpeed = 1f;
                }

                Vector2 input = aiPath.velocity.normalized;
                SetMinotaursAnimator(minotaurAnimator, animationSpeed, input);

                if (distance < 2f)
                {
                    _eventWorld.Value.SetEvent<GameOverEvent>();
                    _targetPool.Value.Del(minotaurEntity);
                    minotaurDestination.enabled = false;
                    aiPath.canMove = false;
                    SetMinotaursAnimator(minotaurAnimator, 1f, Vector2.zero);
                    minotaurAnimator.Play(MinotaurAnimatorVariables.KillHero);
                }
            }
        }

        private static void SetMinotaursAnimator(Animator minotaurAnimator, float animationSpeed, Vector2 input)
        {
            minotaurAnimator.speed = animationSpeed;
            minotaurAnimator.SetBool(AnimatorVariables.IsWalking, input != Vector2.zero);
            minotaurAnimator.SetFloat(AnimatorVariables.HorizontalInput, input.x);
            minotaurAnimator.SetFloat(AnimatorVariables.VerticalInput, input.y);
        }
    }
}