using System.Collections;
using UnityEngine;

public class ErabyFallState : ErabyGenericFallState
{
    private float resetDiveVelocityY = 0f;

    private bool isGliding = false;
    private bool isDiving = false;

    private bool isFrozen = false;

    private Coroutine glideRoutine = null;

    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        onGenericFallStateEnter();
        controls.DiveStarted += goToFastFall;
        controls.DiveReleased += onFastFallCancel;
        controls.JumpPressed += onGlide;
        controls.JumpReleased += onGlideCancel;
        isDiving = false;
        isGliding = false;

        if (controls.isDiving())
            goToFastFall();

        if (controls.isJumping())
            isGliding = true;
    }

    protected override void onStateExit()
    {
        controls.DiveStarted -= goToFastFall;
        controls.DiveReleased -= onFastFallCancel;
        controls.JumpPressed -= onGlide;
        controls.JumpReleased -= onGlideCancel;

        this.DisposeCoroutine(ref landingRoutine);
        onGenericFallStateExit();
        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
        if (isFrozen)
        {
            body.SetVelocityY(0);
        }
        else if (isGliding)
        {
            float newVelocityY =
                body.VelocityY + accelerationData.GlideDecelerationY * Time.fixedDeltaTime;
            body.SetVelocityY(newVelocityY);
        }
        else if (isDiving)
        {
            float newVelocityY =
                body.VelocityY - accelerationData.DiveAccelrationY * Time.fixedDeltaTime;
            body.SetVelocityY(newVelocityY);
        }
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }
    #endregion

    #region PRIVATE
    protected override IEnumerator landingSequence(string i_tag)
    {

        persistentData.landingVelocityX = body.VelocityX;

        body.SetVelocityX(0);
        body.SetVelocityY(0);
        controls.DisableControls();

        HaraPlatformAbstract platform = getCollidedPlatformComponent();

        yield return this.Wait(timeBeforeBounce);

        if (platform != null)
            platform.onCollision();

        if (i_tag == "Bouncy")
        {
            bounceState.SetFallPlatform(platform);
            setState<ErabyBounceState>();
        }
        else
            setState<ErabyCrashState>();

        this.DisposeCoroutine(ref landingRoutine);
    }

    private IEnumerator glideSequence()
    {
        isFrozen = true;
        body.SetVelocityY(0);
        yield return this.Wait(0.4f);
        isFrozen = false;
        isGliding = true;
        this.DisposeCoroutine(ref glideRoutine);
    }

    void goToFastFall()
    {
        isGliding = false;
        isDiving = true;
    }

    void onFastFallCancel()
    {
        isDiving = false;
    }

    void onGlide()
    {
        if (isGliding || glideRoutine != null)
            return;
        glideRoutine = StartCoroutine(glideSequence());
    }

    void onGlideCancel()
    {
        isGliding = false;
    }
    #endregion
}
