
using UnityEngine;

public class ErabyFallState : ErabyAbstractFallState
{
    private Coroutine glideRoutine = null;

    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter fall");
        controls.DiveStarted += goToFastFall;
        controls.JumpPressed += goToGlide;

        if (controls.isDiving())
            goToFastFall();

        if (controls.isJumping())
            goToGlide();
    }

    protected override void onStateExit()
    {
        controls.DiveStarted -= goToFastFall;

        controls.JumpPressed -= goToGlide;

        base.onStateExit();
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }
    #endregion

    #region PRIVATE


    protected override void onDidEnterGround()
    {
        goToLanding<ErabyCrashState>();
    }

    void goToFastFall()
    {
        setState<ErabyDiveState>();
    }

    void goToGlide()
    {
        setState<ErabyGlideStartState>();
    }

    #endregion
}
