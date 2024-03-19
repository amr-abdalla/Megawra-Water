using UnityEngine;
using System.Collections;

class ErabySmallLaunchState : ErabyAbstractLaunchState
{
    // [SerializeField]
    // private float launchVelocityY = 5f;

    [SerializeField]
    private float maxJumpHeight = 5f;

    [SerializeField]
    private float launchVelocityX = 3f;

    protected override void onStateEnter()
    {
        dataProvider.launchVelocityY = float.PositiveInfinity;

        dataProvider.PlayerJumpHeight = maxJumpHeight;
        dataProvider.launchVelocityX = launchVelocityX;
        base.onStateEnter();
    }

    protected override void goToJump()
    {
        setState<ErabySmallJumpState>();
    }
}
