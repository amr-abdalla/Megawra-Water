using System.Collections;
using UnityEngine;

public class ErabyAbstractJumpState : MoveHorizontalAbstractState
{
    protected float initialVelocityY = 0f;

    #region STATE API
    protected override void onStateInit() { }

    private Vector3[] gizmoPoints = new Vector3[2];

    protected override void onStateEnter()
    {
        controls.EnableControls();
        initialVelocityY = body.VelocityY;
        gizmoPoints[0] = new Vector3(-100, persistentData.jumpStopHeight, 0);
        gizmoPoints[1] = new Vector3(100, persistentData.jumpStopHeight, 0);
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
            goToFall();
            return;
        }
    }

    public override void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gizmoPoints[0], gizmoPoints[1]);
    }

    protected virtual void goToFall()
    {
        setState<ErabyFallState>();
    }

    #endregion
}
