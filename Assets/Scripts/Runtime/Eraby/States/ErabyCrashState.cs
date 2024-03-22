// using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErabyCrashState : ErabyAbstractLandingState
{
    [SerializeField]
    private float minBounceVelocityY = 0f;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter crash");
        dataProvider.numCrashes += 1;
        if (dataProvider.numCrashes >= 5)
        {
            // TODO: Goto menu
            SceneManager.LoadScene("Main Menu");
        }

        base.onStateEnter();
    }

    protected override IEnumerator landingSequence()
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

    protected override void applyTapMulipier()
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
