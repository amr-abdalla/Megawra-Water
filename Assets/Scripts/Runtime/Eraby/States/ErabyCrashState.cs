// using Cinemachine;
using UnityEngine;
using System.Collections;

public class ErabyCrashState : ErabyAbstractLandingState
{
    [SerializeField]
    private float minBounceVelocityY = 0f;

 

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter crash");

        
        
 
        base.onStateEnter();
    }

    override protected IEnumerator landingSequence()
    {
        yield return new WaitForSeconds(landingTime);

        // Debug.Log("LaunchVelocit")
        if (dataProvider.launchVelocityY > minBounceVelocityY)
        {
            if (tapManager.isTapped())
                setState<ErabySuperLaunchState>();
            else
                setState<ErabyCrashedLaunchState>();
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
