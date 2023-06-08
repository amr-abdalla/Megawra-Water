// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabySmallFallState : ErabyGenericFallState
{
    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        // Debug.Log("Enter fall");

        Debug.Log("Enter Small Fall");
        onGenericFallStateEnter();
    }

    protected override void onStateExit()
    {
        this.DisposeCoroutine(ref landingRoutine);
        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
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
        // Debug.Log("Landing sequence");

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
            setState<ErabyWalkState>();

        this.DisposeCoroutine(ref landingRoutine);
    }
    #endregion
}
