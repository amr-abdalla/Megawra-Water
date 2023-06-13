using UnityEngine;
using System.Collections;

public class ErabyBounceState : ErabyAbstractBounceState
{
    private HaraPlatformAbstract fallPlatform = null;

    #region STATE API
    protected override void onStateEnter()
    {
        // Debug.Log("Enter bounce");

        if (!body.IsGrounded)
        {
            setState<ErabyFallState>();
            return;
        }

        HaraPlatformAbstract collidedPlatform = getCollidedPlatformComponent();

        if (collidedPlatform == null)
            return;

        if (collidedPlatform != fallPlatform)
        {
            collidedPlatform.onCollision();
        }

        initialVelocityY =
            collidedPlatform.BounceVelocityYMultiplier * persistentData.initialVelocityY;

        float newVelocityX =
            -Mathf.Abs(persistentData.landingVelocityX)
            * collidedPlatform.BounceVelocityXMultiplier;
        newVelocityX = -Mathf.Clamp(
            Mathf.Abs(newVelocityX),
            Mathf.Abs(newVelocityX),
            Mathf.Abs(accelerationData.MaxVelocityX)
        );
        body.SetVelocityX(newVelocityX);
        Debug.Log("Bounce velocity: " + initialVelocityY);
        base.onAbstractBounceStateEnter();
    }
    #endregion

    #region PRIVATE


    private HaraPlatformAbstract getCollidedPlatformComponent()
    {
        //Debug.LogError("Get component bounce");

        HaraPlatformAbstract collidedPlatform =
            body.CurrentGroundTransform.gameObject.GetComponentInParent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponentInChildren<HaraPlatformAbstract>();

        if (body.CurrentGroundTransform.gameObject.tag == "Finish")
        {
            setState<ErabyCrashState>();
        }
        else if (collidedPlatform == null)
        {
            collidedPlatform = fallPlatform;
            if (collidedPlatform == null)
            {
                Debug.LogError("No hara platform component found! setting state to idle.");
                setState<ErabyCrashState>();
            }
        }

        return collidedPlatform;
    }

    public void SetFallPlatform(HaraPlatformAbstract i_platform)
    {
        fallPlatform = i_platform;
    }

    #endregion
}
