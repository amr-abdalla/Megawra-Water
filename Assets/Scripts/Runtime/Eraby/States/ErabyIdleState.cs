using System.Collections;
using UnityEngine;

public class ErabyIdleState : State
{
    protected override void onStateEnter()
    {
        if (controls == null)
            return;
        if (controls.isJumping())
            handleJumpPressed();
        if (controls.isMoving())
            handleMoveStarted();
        controls.MoveStarted += handleMoveStarted;
        controls.JumpPressed += handleJumpPressed;
    }

    protected override void onStateExit()
    {
        controls.MoveStarted -= handleMoveStarted;
        controls.JumpPressed -= handleJumpPressed;
    }

    protected override void onStateFixedUpdate() { }

    protected override void onStateUpdate() { }

    public override void ResetState() { }

    protected override void onStateInit() { }

    void handleMoveStarted(float x = 0)
    {
        stateMachine.SetState<ErabyWalkState>();
    }

    void handleJumpPressed()
    {
        stateMachine.SetState<ErabyJumpState>();
    }
}
