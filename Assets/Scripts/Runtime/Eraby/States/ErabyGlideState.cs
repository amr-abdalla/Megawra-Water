using UnityEngine;

public class ErabyGlideState : ErabyAbstractFallState
{
    [SerializeField]
    private float gravityMod = 0.5f;

    private float originalGravityMod;
    protected override void onStateEnter()
    {
        Debug.Log("Enter glide");
        controls.EnableControls();
        controls.JumpReleased += goToFall;
        originalGravityMod = body.GravityConfig.GravityModifier;
        body.SetGravityModifier(gravityMod);

        base.onStateEnter();

        if (!controls.isJumping())
            goToFall();

                    

        
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
