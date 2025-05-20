using UnityEngine;

public class PlatformMoveState : State
{
    [SerializeField]
    private Platform platformObj;

    [SerializeField]
    private AccelerationConfig2D accelerationConfig;

    private Rigidbody2D moveTarget;

    protected override void onStateInit()
    {
        moveTarget = platformObj.GetComponent<Rigidbody2D>();
    }

    protected override void onStateEnter()
    {
        // Debug.Log("Platform Move State");
        platformObj.onCollision += onPlatformCollision;
        moveTarget.velocity = accelerationConfig.MoveVelocityX * Vector2.left;
    }

    protected override void onStateExit()
    {
        platformObj.onCollision -= onPlatformCollision;
        moveTarget.velocity = Vector2.zero;
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    public override void ResetState() { }

    void onPlatformCollision(PlatformCollisionData i_collisionParams)
    {
        if (i_collisionParams.direction.x != 0)
            setState<PlatformBumpState>();
        else
            setState<PlatformBounceState>();
    }
}
