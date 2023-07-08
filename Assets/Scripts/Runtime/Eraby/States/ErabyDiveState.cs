using System.Collections;
using UnityEngine;

public class ErabyDiveState : ErabyAbstractFallState
{
    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter dive");
        controls.DiveReleased += goToFall;
        controls.JumpPressed += goToGlide;
    }

    protected override void onStateExit()
    {
        controls.DiveReleased -= goToFall;
        controls.JumpPressed -= goToGlide;
        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        if (!isEnabled)
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

    private void goToGlide()
    {
        stateMachine.SetState<ErabyGlideStartState>();
    }

    private void goToFall()
    {
        stateMachine.SetState<ErabyFallState>();
    }

    protected override void onDidEnterGround()
    {
        goToLanding<ErabyCrashState>();
    }
}
