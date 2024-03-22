using System.Collections;
using UnityEngine;

class ErabyLevelStartFallState : ErabyAbstractLandingState
{
    [SerializeField]
    private float launchVelX = -3f;

    private float initialGravity = 0f;

    #region STATE API
    protected override void onStateEnter()
    {
        initialGravity = body.GravityConfig.GravityModifier;
        body.SetGravityModifier(0);
        dataProvider.launchVelocityY = float.PositiveInfinity;
        dataProvider.launchVelocityX = launchVelX;

        base.onStateEnter();
    }

    #endregion

    #region PRIVATE


    override protected IEnumerator landingSequence()
    {
        yield return new WaitForSeconds(landingTime);

        // else
        setState<ErabyStartLaunchState>();
    }

    protected override void applyTapMulipier() { }

    protected override void onStateExit()
    {
        body.SetGravityModifier(initialGravity);
        base.onStateExit();
    }

    #endregion
}
