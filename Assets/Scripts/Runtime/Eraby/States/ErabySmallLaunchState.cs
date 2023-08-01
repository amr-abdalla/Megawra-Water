using UnityEngine;
using System.Collections;

class ErabySmallLaunchState : ErabyAbstractLaunchState
{
    [SerializeField]
    private float launchVelocityY = 5f;

    protected override void onStateEnter()
    {
        dataProvider.launchVelocityY = launchVelocityY;
        base.onStateEnter();
    }

    protected override void goToJump()
    {
        setState<ErabySmallJumpState>();
    }
}
