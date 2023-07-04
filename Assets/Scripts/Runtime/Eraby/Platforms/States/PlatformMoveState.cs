using UnityEngine;

public class PlatformMoveState : State
{
    [SerializeField]
    private PlatformCollisionEvents collisionEvents;

    [SerializeField]
    private AccelerationConfig2D accelerationConfig;

    [SerializeField]
    private Transform moveTarget;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Move State");
        collisionEvents.OnBounce += goToBounce;
        collisionEvents.OnBump += goToBump;
    }

    protected override void onStateExit()
    {
        collisionEvents.OnBounce -= goToBounce;
        collisionEvents.OnBump -= goToBump;
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

    private void goToBounce()
    {
        setState<PlatformBounceState>();
    }

    private void goToBump(Vector2 bumpDirection)
    {
        setState<PlatformBumpState>();
    }
}
