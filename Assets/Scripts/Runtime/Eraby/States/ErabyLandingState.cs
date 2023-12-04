using UnityEngine;
using System.Collections;

public class ErabyLandingState : ErabyAbstractLandingState
{

    private float maxLandingTime = 1f;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        dataProvider.launchVelocityY = dataProvider.bounceVelocityMultiplier.y * dataProvider.launchVelocityY;

        float newVelocityX = clampVelocityX(
            -Mathf.Abs(dataProvider.landingVelocityX) * dataProvider.bounceVelocityMultiplier.x
        );
        dataProvider.launchVelocityX = newVelocityX;
        Debug.Log("Bounce velocity: " + dataProvider.launchVelocityY);
        landingTime = mapLaunchVelocityToLandingTimme(dataProvider.launchVelocityY);
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


    private float mapLaunchVelocityToLandingTimme(float yVel){
        float maxYVelocity = accelerationData.MaxVelocityY;
        return yVel/maxYVelocity * maxLandingTime;
    }

    #endregion
}
