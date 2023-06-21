using UnityEngine;
using System.Collections;

public class ErabyLandingState : ErabyAbstractLandingState
{
    private HaraPlatformAbstract fallPlatform = null;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        fallPlatform = persistentData.fallPlatform;
        fallPlatform.onCollision();

        //  HaraPlatformAbstract collidedPlatform = getCollidedPlatformComponent();

        /*   if (collidedPlatform != fallPlatform)
           {
               collidedPlatform.onCollision();
           } */

        launchVelocityY =
            fallPlatform.BounceVelocityYMultiplier * persistentData.initialVelocityY;

        float newVelocityX = clampVelocityX(
            -Mathf.Abs(persistentData.landingVelocityX) * fallPlatform.BounceVelocityXMultiplier
        );
        persistentData.launchVelocityY = launchVelocityY;
        persistentData.launchVelocityX = newVelocityX;
        Debug.Log("Bounce velocity: " + launchVelocityY);
        // landingRoutine = StartCoroutine(landingSequence());
        base.onStateEnter();
    }

    #endregion

    #region PRIVATE


    override protected IEnumerator landingSequence()
    {
        yield return new WaitForSeconds(landingTime);
        if (tapManager.isTapped())
            setState<ErabySuperLaunchState>();
        else
            setState<ErabyLaunchState>();
    }

    override protected void applyTapMulipier()
    {
        //Debug.LogError("Apply tap multiplier");
        float newVelocityX = clampVelocityX(
            -Mathf.Abs(persistentData.landingVelocityX) * tapMultiplier
        );

        persistentData.launchVelocityX = newVelocityX;
        persistentData.launchVelocityY *= tapMultiplier;
    }

   /* private HaraPlatformAbstract getCollidedPlatformComponent()
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

        // if (body.CurrentGroundTransform.gameObject.tag == "Finish")
        // {
        //     setState<ErabyCrashState>();
        // }
        // else if (collidedPlatform == null)
        // {
        //     collidedPlatform = fallPlatform;
        //     if (collidedPlatform == null)
        //     {
        //         Debug.LogError("No hara platform component found! setting state to idle.");
        //         setState<ErabyCrashState>();
        //     }
        // }

        return collidedPlatform;
    } */

    #endregion
}
