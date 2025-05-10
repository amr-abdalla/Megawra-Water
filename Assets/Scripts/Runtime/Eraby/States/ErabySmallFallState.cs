using UnityEngine;

public class ErabySmallFallState : ErabyAbstractFallState
{
    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        dataProvider.initialVelocityX = 0;
        Debug.Log("Enter Small Fall");
        base.onStateEnter();
    }

    protected override void onStateExit()
    {
        onGenericFallStateExit();

        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }
    #endregion

    protected override void onDidEnterGround()
    {
        if (controls.isMoving())
            goToLanding<ErabyWalkState>();
        else goToLanding<ErabyIdleState>();
    }
}
