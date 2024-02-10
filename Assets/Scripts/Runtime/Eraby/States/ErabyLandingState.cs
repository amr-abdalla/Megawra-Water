using UnityEngine;
using System.Collections;

public class ErabyLandingState : ErabyAbstractLandingState
{
    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        launchVelocityY = dataProvider.bounceVelocityMultiplier.y * dataProvider.initialVelocityY;

        float newVelocityX = clampVelocityX(
            -Mathf.Abs(dataProvider.landingVelocityX) * dataProvider.bounceVelocityMultiplier.x
        );
        dataProvider.launchVelocityY = launchVelocityY;
        dataProvider.launchVelocityX = newVelocityX;
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
            -Mathf.Abs(dataProvider.landingVelocityX) * tapMultiplier
        );

        dataProvider.launchVelocityX = newVelocityX;
        dataProvider.launchVelocityY *= tapMultiplier;
    }

    #endregion
}
