using System.Collections;
using UnityEngine;

public class ErabyAbstractJumpState : MoveHorizontalAbstractState
{
    [Header("Jump Configs")]
    [SerializeField]
    protected PersistentErabyData persistentData = null;

    [SerializeField]
    protected float initialVelocityY = 0f;

    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        controls.EnableControls();
        initialVelocityY = body.VelocityY;
        base.onStateEnter();
    }

    protected override void onStateExit()
    {
        persistentData.initialVelocityY = initialVelocityY;

        base.onStateExit();
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();

        onJumpEnded();
    }

    public override void ResetState()
    {
        base.ResetState();

        StopAllCoroutines();

        onStateExit();
    }
    #endregion

    #region PROTECTED

    protected virtual void onJumpEnded()
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

    #endregion
}
