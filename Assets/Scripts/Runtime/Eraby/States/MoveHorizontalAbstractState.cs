using UnityEngine;
using System.Collections;

public abstract class MoveHorizontalAbstractState : State
{
    [SerializeField]
    protected AccelerationConfig2D accelerationData = null;

    [SerializeField]
    protected PhysicsBody2D body = null;

    protected float initialVelocityX = 0f;

    private Coroutine accelerationCoroutine = null;

    #region PROTECTED

    protected override void onStateEnter()
    {
        initialVelocityX = persistentData.initialVelocityX;
        if (controls.isMoving())
            updateMoveVelocity(controls.MoveDirection());
        controls.MoveStarted += updateMoveVelocity;
        controls.MoveReleased += handleMoveCancel;
    }

    protected override void onStateExit()
    {
        controls.MoveStarted -= updateMoveVelocity;
        controls.MoveReleased -= handleMoveCancel;
        this.DisposeCoroutine(ref accelerationCoroutine);
    }

    protected virtual void onDidStop() { }

    #endregion

    #region PRIVATE

    float clampVelocityX(float i_velocityX, float i_maxVelocityX)
    {
        return Mathf.Abs(i_velocityX) >= i_maxVelocityX
            ? Mathf.Sign(i_velocityX) * i_maxVelocityX
            : i_velocityX;
    }

    protected void updateMoveVelocity(float x)
    {
        if (!isEnabled)
            return;

        if (accelerationCoroutine != null)
            this.DisposeCoroutine(ref accelerationCoroutine);

        accelerationCoroutine = StartCoroutine(accelerationSequence(x));
    }

    private void handleMoveCancel()
    {
        updateMoveVelocity(0);
    }

    private IEnumerator accelerationSequence(float direction)
    {
        bool isDecelerating = true;
        float targetVelocityX = initialVelocityX + direction * accelerationData.MoveVelocityX;
        targetVelocityX = clampVelocityX(targetVelocityX, accelerationData.MaxVelocityX);

        if (Mathf.Abs(body.VelocityX) < Mathf.Abs(targetVelocityX))
            isDecelerating = false;

        float acceleration = isDecelerating
            ? accelerationData.DecelerationX
            : accelerationData.AccelerationX;
        while (
            Mathf.Abs(body.VelocityX) < Mathf.Abs(targetVelocityX) && !isDecelerating
            || Mathf.Abs(body.VelocityX) > Mathf.Abs(targetVelocityX) && isDecelerating
        )
        {
            if (!isEnabled)
                break;
            body.SetVelocityX(body.VelocityX + direction * acceleration * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        this.DisposeCoroutine(ref accelerationCoroutine);
    }

    protected override void onStateFixedUpdate() { }

    #endregion

    public override void ResetState() { }
}
