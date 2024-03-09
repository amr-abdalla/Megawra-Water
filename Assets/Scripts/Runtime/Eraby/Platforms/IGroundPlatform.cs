public interface IGroundPlatform
{
    public float BounceVelocityYMultiplier { get; }

    public float BounceVelocityXMultiplier { get; }

    public bool IsBouncy => BounceVelocityYMultiplier >= 1f;

    public float PlayerJumpHeight { get; }
}
