using System.Collections;
using UnityEngine;

public class ErabyFallState : ErabyAbstractFallState
{
    private Coroutine glideRoutine = null;

    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter fall");
        controls.DiveStarted += goToFastFall;
        controls.JumpPressed += goToGlide;

        if (controls.isDiving())
            goToFastFall();

        if (controls.isJumping())
            goToGlide();
    }

    protected override void onStateExit()
    {
        controls.DiveStarted -= goToFastFall;

        controls.JumpPressed -= goToGlide;

        this.DisposeCoroutine(ref landingRoutine);

        base.onStateExit();
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }
    #endregion

    #region PRIVATE

    // move to a new landing state
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
            persistentData.fallPlatform = platform;
            setState<ErabyBounceState>();
        }
        else
            setState<ErabyCrashState>();

        this.DisposeCoroutine(ref landingRoutine);
    }

    void goToFastFall()
    {
        setState<ErabyDiveState>();
    }

    void goToGlide()
    {
        setState<ErabyGlideStartState>();
    }

    #endregion
}
