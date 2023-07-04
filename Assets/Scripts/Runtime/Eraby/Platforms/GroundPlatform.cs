using UnityEngine;

public class GroundPlatform : Platform
{
    [SerializeField]
    [Range(1f, 30f)]
    private float maxJumpHeight = 8f;

    [SerializeField]
    private PlatformVerticalData bounceData = null;

    public float BounceVelocityYMultiplier => bounceData.BounceVelocityYMultiplier;

    public float BounceVelocityXMultiplier => bounceData.BounceVelocityXMultiplier;

    public bool IsBouncy => BounceVelocityYMultiplier >= 1f;

    public float MaxJumpHeight => maxJumpHeight;

    protected override PlatformCollisionData generateCollisionData(Collision2D other)
    {
        PlatformCollisionData ret = new PlatformCollisionData();

        ret.direction = MathConstants.VECTOR_2_UP;

        return ret;
    }


}
