using UnityEngine;

public class ErabyIdleState : ErabyState
{
    [SerializeField]
    protected PhysicsBody2D body = null;

    protected override void onStateEnter()
    {
        body.SetVelocityX(0);
        Debug.Log("Enter idle");
        if (controls == null)
        {
            Debug.LogError("Controls not set");
            return;
        }

        controls.EnableControls();
        if (controls.isJumping())
            handleJumpPressed();

        controls.MoveStarted += handleMoveStarted;
        controls.JumpPressed += handleJumpPressed;

        if (controls.isMoving())
            handleMoveStarted();
        else
            body.SetVelocityX(0);
    }

    protected override void onStateExit()
    {
        controls.MoveStarted -= handleMoveStarted;
        controls.JumpPressed -= handleJumpPressed;
    }

    protected override void onStateFixedUpdate() { }

    protected override void onStateUpdate() { }

    public override void ResetState()
    {
        onStateExit();
    }

    protected override void onStateInit() { }

    void handleMoveStarted(float x = 0)
    {
        if (!isEnabled)
            return;
        setState<ErabyWalkState>();
    }

    void handleJumpPressed()
    {
        if (!isEnabled)
            return;
        setState<ErabySmallLaunchState>();
    }
}
