using UnityEngine;

abstract class ErabyAbstractLaunchState : ErabyState
{
    [Header("Jump Configs")]
    [SerializeField]
    [Range(1f, 30f)]
    protected float maxJumpHeight = 8f;

    [SerializeField]
    protected PhysicsBody2D body = null;
    private float initialVelocityY = 0f;
    private float startJumpY = 0f;
    private float stopJumpY = 0f;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        initialVelocityY = dataProvider.launchVelocityY;
        startJumpY = body.transform.position.y;
        stopJumpY = startJumpY + maxJumpHeight;
        initialVelocityY = clampVelocityY(initialVelocityY);
        Debug.Log(initialVelocityY);
        body.SetVelocityY(initialVelocityY);
        body.SetVelocityX(dataProvider.launchVelocityX);
        dataProvider.launchVelocityY = initialVelocityY;
        dataProvider.initialVelocityX = dataProvider.launchVelocityX;
        Debug.Log(body.VelocityY);
        goToJump();
    }

    protected override void onStateExit()
    {
        dataProvider.launchVelocityY = 0f;
        dataProvider.launchVelocityX = 0f;
        dataProvider.jumpStopHeight = stopJumpY;
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    public override void ResetState()
    {
        onStateExit();
    }

    void clampInitialVelocityY()
    {
        initialVelocityY = clampVelocityY(initialVelocityY);
    }

    protected float clampVelocityY(float velocityY)
    {
        // given a desceleration, clamp the initial velocity to reach the desired height
        // u = sqrt(2as)
        float maxInitialVelocityY = Mathf.Sqrt(
            2
                * body.GravityVector.magnitude
                * (maxJumpHeight - (body.transform.position.y - startJumpY))
        );
        return Mathf.Clamp(velocityY, velocityY, maxInitialVelocityY);
    }

    abstract protected void goToJump();

    public override void DrawGizmos()
    {
        base.DrawGizmos();
        // Draw a line gizmo for max height
    }
}
