// using Cinemachine;
using UnityEngine;
using System.Collections;

public class ErabyCrashState : ErabyAbstractLandingState
{
    [SerializeField]
    private float minBounceVelocityY = 0f;

    [SerializeField]
    private float velocityXMultiplier = 0.5f;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter crash");

        launchVelocityY = dataProvider.initialVelocityY / 2;
        float newVelocityX = -Mathf.Abs(dataProvider.landingVelocityX) * velocityXMultiplier;
        newVelocityX = -Mathf.Clamp(
            Mathf.Abs(newVelocityX),
            Mathf.Abs(newVelocityX),
            Mathf.Abs(accelerationData.MaxVelocityX)
        );
        dataProvider.launchVelocityX = newVelocityX;
        dataProvider.launchVelocityY = launchVelocityY;
        base.onStateEnter();
    }

    override protected IEnumerator landingSequence()
    {
        yield return new WaitForSeconds(landingTime);

        if (launchVelocityY > minBounceVelocityY)
        {
            if (tapManager.isTapped())
                setState<ErabySuperLaunchState>();
            else
                setState<ErabySmallLaunchState>();
        }
        else
            setState<ErabyIdleState>();
    }

    override protected void applyTapMulipier()
    {
        //Debug.LogError("Apply tap multiplier");
        float newVelocityX = clampVelocityX(
            -Mathf.Abs(dataProvider.landingVelocityX) * tapMultiplier
        );

        dataProvider.launchVelocityX = newVelocityX;
        dataProvider.launchVelocityY *= tapMultiplier;
    }

    #endregion

    #region PRIVATE

    #endregion
}
