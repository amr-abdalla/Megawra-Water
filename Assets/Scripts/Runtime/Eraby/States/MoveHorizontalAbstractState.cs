using System.Collections;
using UnityEngine;

public abstract class MoveHorizontalAbstractState : ErabyState
{
    [SerializeField]
    protected PhysicsBody2D body = null;

    protected float initialVelocityX = 0f;

    private Coroutine accelerationCoroutine = null;

    #region PROTECTED

    protected override void onStateEnter()
    {
        Debug.Log("dataProvider.initialVelocityX: " + dataProvider.initialVelocityX);
        initialVelocityX = dataProvider.initialVelocityX;
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
        if (i_velocityX > 0) return 0;
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
            // Debug.Log(
            //     "body.VelocityX: "
            //         + body.VelocityX
            //         + " targetVelocityX: "
            //         + targetVelocityX
            //         + " direction: "
            //         + direction
            //         + " acceleration: "
            //         + acceleration
            // );

            // body.SetVelocityX(body.VelocityX + direction * acceleration * Time.fixedDeltaTime);
            body.SetVelocityX(
                Mathf.Lerp(body.VelocityX, targetVelocityX, acceleration * Time.fixedDeltaTime)
            );
            // body.SetVelocityX(targetVelocityX);
            yield return new WaitForFixedUpdate();
        }
        this.DisposeCoroutine(ref accelerationCoroutine);
    }

    protected override void onStateFixedUpdate() { }

    #endregion

    public override void ResetState() { }
}
