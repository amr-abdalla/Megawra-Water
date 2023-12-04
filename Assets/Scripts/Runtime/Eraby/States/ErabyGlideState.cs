using UnityEngine;

public class ErabyGlideState : ErabyAbstractFallState
{
    [SerializeField]
    private float gravityMod = 0.5f;

    private float originalGravityMod;
    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter glide");
        controls.EnableControls();
        controls.JumpReleased += goToFall;
        originalGravityMod = body.GravityConfig.GravityModifier;
        body.SetGravityModifier(gravityMod);
        if (!controls.isJumping())
            goToFall();

        
    }

    protected override void onStateFixedUpdate()
    {
        if (!isEnabled)
            return;
        // float newVelocityY = body.VelocityY + accelerationData.DecelerationY * Time.fixedDeltaTime;
        // Debug.Log(newVelocityY);
        // body.SetVelocityY(newVelocityY);

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
        body.SetGravityModifier(originalGravityMod);
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
