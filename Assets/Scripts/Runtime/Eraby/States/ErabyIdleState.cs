using System.Collections;
using UnityEngine;

public class ErabyIdleState : State
{
    private bool active = false;

    [SerializeField]
    protected PhysicsBody2D body = null;

    protected override void onStateEnter()
    {
        Debug.Log("Enter idle");
        active = true;
        if (controls == null)
        {
            Debug.LogError("Controls not set");
            return;
        }
        body.SetVelocityX(0);
        controls.EnableControls();
        if (controls.isJumping())
            handleJumpPressed();
        if (controls.isMoving())
            handleMoveStarted();
        controls.MoveStarted += handleMoveStarted;
        controls.JumpPressed += handleJumpPressed;
    }

    protected override void onStateExit()
    {
        active = false;
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
        if (!active)
            return;
        stateMachine.SetState<ErabyWalkState>();
    }

    void handleJumpPressed()
    {
        if (!active)
            return;
        stateMachine.SetState<ErabySmallLaunchState>();
    }
}
