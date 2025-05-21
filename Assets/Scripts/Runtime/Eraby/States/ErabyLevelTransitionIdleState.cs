
using System.Collections;
using UnityEngine;

class ErabyLevelTransitionIdleState : ErabyAbstractLandingState
{

    private float initialGravity = 0f;

    #region STATE API
    protected override void onStateEnter()
    {
	Debug.Log("TIDLE");
        initialGravity = body.GravityConfig.GravityModifier;
        body.SetGravityModifier(0);
        base.onStateEnter();
    }

    #endregion

    #region PRIVATE


    override protected IEnumerator landingSequence()
    {
        yield return null;

    }

    protected override void applyTapMulipier() { }

    protected override void onStateExit()
    {
        body.SetGravityModifier(initialGravity);
        base.onStateExit();
    }

    #endregion
}
