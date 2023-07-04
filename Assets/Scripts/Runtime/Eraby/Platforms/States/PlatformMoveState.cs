using UnityEngine;

public class PlatformMoveState : State
{
    [SerializeField]
    private Platform platformObj;

    [SerializeField]
    private AccelerationConfig2D accelerationConfig;

    [SerializeField]
    private Transform moveTarget;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Move State");
        platformObj.onCollision += onPlatformCollision;
    }

    protected override void onStateExit()
    {
        platformObj.onCollision -= onPlatformCollision;
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        if (accelerationConfig != null && isEnabled)
        {
            moveTarget.position =
                moveTarget.position
                + Vector3.left * accelerationConfig.MoveVelocityX * Time.fixedDeltaTime;
        }
    }

    public override void ResetState() { }

    void onPlatformCollision(PlatformCollisionData i_collisionParams)
    {
        if(i_collisionParams.direction.x != 0)
            setState<PlatformBumpState>();
        else
            setState<PlatformBounceState>();

    }

}
