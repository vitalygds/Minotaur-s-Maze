namespace General
{
    public interface IRuntimeData
    {
        float HeroMovementSpeed { get; }
        float HeroDashTime { get; }
        float HeroDashCoolDownTime { get; }
        float HeroDashSpeedMultiplier { get;}
    }
}