using System.Collections;
using UnityEngine;

public class ErabyGlideState : MoveHorizontalAbstractState
{
    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter glide");
        controls.EnableControls();
        active = true;
        controls.JumpReleased += goToFall;
        if (!controls.isJumping())
            goToFall();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        if (!active)
            return;
        float newVelocityY = body.VelocityY + accelerationData.DecelerationY * Time.fixedDeltaTime;
        body.SetVelocityY(newVelocityY);

        base.onStateFixedUpdate();
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }

    protected override void onStateExit()
    {
        active = false;
        controls.JumpReleased -= goToFall;
        base.onStateExit();
    }

    private void goToFall()
    {
        stateMachine.SetState<ErabyFallState>();
    }
}
