using UnityEngine;

public abstract class HaraPlatformAbstract : MonoBehaviourBase
{
    [SerializeField]
    [Range(1f, 30f)]
    private float maxJumpHeight = 8f;

    [SerializeField]
    protected Collider2D thisCollider = null;

    [SerializeField]
    private PlatformBounceData bounceData = null;

    public float BounceVelocityYMultiplier => bounceData.BounceVelocityYMultiplier;

    public float BounceVelocityXMultiplier => bounceData.BounceVelocityXMultiplier;

    public float MaxJumpHeight => maxJumpHeight;

    public virtual void onCollision()
    {
        //Play animations, sound, etc..
    }

    public void EnableCollider(bool i_enable)
    {
        thisCollider.enabled = i_enable;
    }
}
