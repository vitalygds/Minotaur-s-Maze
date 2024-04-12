using UnityEngine;

namespace MyGame.General.Data
{
    public interface IRuntimeData
    {
        float HeroMovementSpeed { get; }
        float HeroDashTime { get; }
        float HeroDashCoolDownTime { get; }
        float HeroDashSpeedMultiplier { get;}
    }
}