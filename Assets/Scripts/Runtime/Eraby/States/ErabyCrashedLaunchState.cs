using UnityEngine;
using System.Collections;
using System;

class ErabyCrashedLaunchState : ErabyAbstractLaunchState
{
    [SerializeField]
    private float crashFactor = 0.1f;
    protected override void onStateEnter()
    {
        Debug.Log("Enter crashed launch");
        dataProvider.launchVelocityY *= crashFactor;
        float newVelocityX = -Mathf.Abs(dataProvider.landingVelocityX) * crashFactor;
        newVelocityX = -Mathf.Clamp(
            Mathf.Abs(newVelocityX),
            Mathf.Abs(newVelocityX),
            Mathf.Abs(accelerationData.MaxVelocityX)
        );
        dataProvider.launchVelocityX = newVelocityX;
        base.onStateEnter();
    }

    protected override void goToJump()
    {
        setState<ErabySmallJumpState>();
    }
}
