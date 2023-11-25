using System.Collections;
using UnityEngine;

public class ErabyWalkState : MoveHorizontalAbstractState
{
    protected override void onStateEnter()
    {
        Debug.Log("Enter walk");

        if (!controls.isMoving())
            goToIdle();

        dataProvider.initialVelocityX = 0;
        base.onStateEnter();
        controls.EnableControls();
        controls.JumpPressed += goToJump;
        controls.MoveReleased += goToIdle;
    }

    protected override void onStateExit()
    {
        controls.JumpPressed -= goToJump;
        controls.MoveReleased -= goToIdle;
        base.onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    private void goToJump()
    {
        dataProvider.launchVelocityX = body.VelocityX;
        stateMachine.SetState<ErabySmallLaunchState>();
    }

    private void goToIdle()
    {
        Debug.Log("Go to idle");
        // body.SetVelocityX(0);
        stateMachine.SetState<ErabyIdleState>();
    }
}
