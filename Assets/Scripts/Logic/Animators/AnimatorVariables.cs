using UnityEngine;

namespace MyGame.Logic.Animators
{
    internal static class AnimatorVariables
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        public static readonly int VerticalInput = Animator.StringToHash("VerticalInput");
    }

    internal static class MinotaurAnimatorVariables
    {
        public static readonly int KillHero = Animator.StringToHash("KillHero");
    }
    internal static class SpikesAnimatorVariables
    {
        public static readonly int Hide = Animator.StringToHash("Spikes_Hide");
        public static readonly int Show = Animator.StringToHash("Spikes_Show");
    }
}