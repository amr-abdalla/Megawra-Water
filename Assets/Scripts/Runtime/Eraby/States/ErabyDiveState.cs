using System.Collections;
using UnityEngine;

public class ErabyDiveState : MoveHorizontalAbstractState
{
    protected override void onStateEnter()
    {
        active = true;
        base.onStateEnter();
        Debug.Log("Enter dive");
        controls.DiveReleased += goToFall;
        controls.JumpPressed += goToGlide;
    }

    protected override void onStateExit()
    {
        active = false;
        controls.DiveReleased -= goToFall;
        controls.JumpPressed -= goToGlide;
        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        if (!active)
            return;
        float newVelocityY = body.VelocityY - accelerationData.AccelerationY * Time.fixedDeltaTime;
        body.SetVelocityY(newVelocityY);

        base.onStateFixedUpdate();
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    private void goToGlide()
    {
        stateMachine.SetState<ErabyGlideStartState>();
    }

    private void goToFall()
    {
        stateMachine.SetState<ErabyFallState>();
    }
}
