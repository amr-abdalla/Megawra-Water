// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabySmallFallState : ErabyAbstractFallState
{
    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Enter Small Fall");
        base.onStateEnter();
    }

    protected override void onStateExit()
    {
        onGenericFallStateExit();

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

    protected override void goToLanding(string i_tag)
    {
        Debug.Log("Landing sequence");
        persistentData.landingVelocityX = body.VelocityX;

        body.SetVelocityX(0);
        body.SetVelocityY(0);
        controls.DisableControls();

        HaraPlatformAbstract platform = getCollidedPlatformComponent();

        if (platform != null)
            platform.onCollision();

        if (i_tag == "Bouncy")
        {
            persistentData.fallPlatform = platform;
            setState<ErabyLandingState>();
        }
        else
            setState<ErabyIdleState>();
    }
    #endregion
}
