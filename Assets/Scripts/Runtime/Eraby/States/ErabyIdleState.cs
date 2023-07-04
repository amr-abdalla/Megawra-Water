using System.Collections;
using UnityEngine;

public class ErabyIdleState : State
{
    [SerializeField]
    protected PhysicsBody2D body = null;

    [SerializeField]
    protected PersistentErabyData persistentData = null;

    public PersistentErabyData _persistentData
    {
        get => persistentData;
        set => persistentData = value;
    }

    protected override void onStateEnter()
    {
        Debug.Log("Enter idle");
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
        stateMachine.SetState<ErabyWalkState>();
    }

    void handleJumpPressed()
    {
        if (!isEnabled)
            return;
        stateMachine.SetState<ErabySmallLaunchState>();
    }
}
