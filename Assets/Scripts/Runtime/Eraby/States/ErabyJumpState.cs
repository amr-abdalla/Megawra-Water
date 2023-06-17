using System.Collections;
using UnityEngine;

public class ErabyJumpState : ErabyAbstractJumpState
{
    #region STATE API


    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter jump");
        controls.DiveStarted += goToFastFall;
    }

    protected override void onStateExit()
    {
        controls.DiveStarted -= goToFastFall;

        base.onStateExit();
    }

    public override void ResetState()
    {
        base.ResetState();

        onStateExit();
    }
    #endregion

    #region PRIVATE


    protected override void checkHeight()
    {
        if (false == enabled)
        {
            return;
        }

        if (body.VelocityY <= 0)
        {
            setState<ErabyFallState>();
            return;
        }
    }

    protected void goToFastFall()
    {
        setState<ErabyDiveState>();
    }

    #endregion
}
