using UnityEngine;

public class ErabyGlideState : ErabyAbstractFallState
{
    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter glide");
        controls.EnableControls();
        controls.JumpReleased += goToFall;
        if (!controls.isJumping())
            goToFall();
    }

    protected override void onStateFixedUpdate()
    {
        if (!isEnabled)
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
        controls.JumpReleased -= goToFall;
        base.onStateExit();
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
