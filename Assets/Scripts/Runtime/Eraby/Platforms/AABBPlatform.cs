using UnityEngine;

public class AABBPlatform : Platform, IGroundPlatform, IWallPlatform
{
    [SerializeField]
    private GroundPlatformData groundData = null;

    [SerializeField]
    private WallPlatformData wallData = null;

    public float BounceVelocityYMultiplier => groundData.BounceVelocityYMultiplier;

    public float BounceVelocityXMultiplier => groundData.BounceVelocityXMultiplier;

    public bool IsBouncy => BounceVelocityYMultiplier >= 1f;

    public float HealthPenalty => wallData.HealthPenalty;

    public float OnBumpTimeDisabled => wallData.timeDisabled;

    public float BumpMagnitude => wallData.bumpMagnitude;

    public float PlayerJumpHeight => groundData.PlayerJumpHeight;

    protected override PlatformCollisionData generateCollisionData(Collision2D other)
    {
        PlatformCollisionData ret = new PlatformCollisionData();

        Vector2 dir = other.GetContact(0).normal;

        // snap the vector to either the x or y axis
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            dir.y = 0f;
        }
        else
        {
            dir.x = 0f;
        }

        dir.Normalize();
        ret.direction = dir;
        ret.tag = other.gameObject.tag;
        return ret;
    }
}
