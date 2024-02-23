using System.Collections;
using UnityEngine;

public class ErabyLandingState : ErabyAbstractLandingState
{
    private float maxLandingTime = 1f;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        dataProvider.launchVelocityY =
            dataProvider.bounceVelocityMultiplier.y * dataProvider.launchVelocityY;


        Vector2 velocity =
            (new Vector2(dataProvider.landingVelocityX, dataProvider.launchVelocityY).magnitude
            * dataProvider.bounceVelocityMultiplier.magnitude) * new Vector2(-1, 2).normalized;


        float newVelocityX = clampVelocityX(
            velocity.x

        );
        dataProvider.launchVelocityY = velocity.y;
        dataProvider.launchVelocityX = newVelocityX;
        Debug.Log("Bounce velocity: " + dataProvider.launchVelocityY);
        landingTime = mapLaunchVelocityToLandingTime(dataProvider.launchVelocityY);
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

    protected override void applyTapMulipier()
    {
        float newVelocityX = clampVelocityX(
            -Mathf.Abs(dataProvider.landingVelocityX) * tapMultiplier
        );

        dataProvider.launchVelocityX = newVelocityX;
        dataProvider.launchVelocityY *= tapMultiplier;
    }

    private float mapLaunchVelocityToLandingTime(float yVel)
    {
        float maxYVelocity = accelerationData.MaxVelocityY;
        return yVel / maxYVelocity * maxLandingTime;
    }

    #endregion
}
