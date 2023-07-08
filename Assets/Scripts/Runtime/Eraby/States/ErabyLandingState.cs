using UnityEngine;
using System.Collections;

public class ErabyLandingState : ErabyAbstractLandingState
{
    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        launchVelocityY =
            persistentData.bounceVelocityMultiplier.y * persistentData.initialVelocityY;

        float newVelocityX = clampVelocityX(
            -Mathf.Abs(persistentData.landingVelocityX) * persistentData.bounceVelocityMultiplier.x
        );
        persistentData.launchVelocityY = launchVelocityY;
        persistentData.launchVelocityX = newVelocityX;
        Debug.Log("Bounce velocity: " + launchVelocityY);

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
        float newVelocityX = clampVelocityX(
            -Mathf.Abs(persistentData.landingVelocityX) * tapMultiplier
        );

        persistentData.launchVelocityX = newVelocityX;
        persistentData.launchVelocityY *= tapMultiplier;
    }

    #endregion
}
