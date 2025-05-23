using System.Collections;
using UnityEngine;

class ErabyStartLaunchState : ErabyAbstractLaunchState
{
    // [SerializeField]
    // private float launchVelocityY = 5f;

    [SerializeField]
    private float maxJumpHeight = 5f;

    protected override void onStateEnter()
    {
        dataProvider.launchVelocityY = float.PositiveInfinity;

        dataProvider.PlayerJumpHeight = maxJumpHeight;
        base.onStateEnter();
    }

    protected override void goToJump()
    {
        setState<ErabySmallJumpState>();
    }
}
