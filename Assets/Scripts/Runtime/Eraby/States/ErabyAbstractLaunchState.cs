using UnityEngine;

abstract class ErabyAbstractLaunchState : ErabyState
{
    [Header("Jump Configs")]



    [SerializeField]
    protected PhysicsBody2D body = null;
    private float initialVelocityY = 0f;
    [SerializeField]
    private float startJumpY = -1f;
    private float stopJumpY = 0f;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        initialVelocityY = dataProvider.launchVelocityY;
        Debug.Log("Launch Velocity: " + dataProvider.landingVelocityY);
        Debug.Log("Max Jump Height" + dataProvider.PlayerJumpHeight);
        startJumpY = startJumpY < 0 ? body.transform.position.y : startJumpY;
        stopJumpY = startJumpY + dataProvider.PlayerJumpHeight;
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
                * (dataProvider.PlayerJumpHeight - (body.transform.position.y - startJumpY))
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
