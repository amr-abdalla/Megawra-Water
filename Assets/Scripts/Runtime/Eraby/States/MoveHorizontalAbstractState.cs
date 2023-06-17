using UnityEngine;

public abstract class MoveHorizontalAbstractState : State
{
    [SerializeField]
    protected AccelerationConfig2D accelerationData = null;

    [SerializeField]
    protected PhysicsBody2D body = null;

    protected float initialVelocityX = 0f;

    protected bool active = false;

    #region PROTECTED

    protected override void onStateEnter()
    {
        active = true;
        initialVelocityX = body.VelocityX;
        if (controls.isMoving())
            updateMoveVelocity(controls.MoveDirection());
        controls.MoveStarted += updateMoveVelocity;
        controls.MoveReleased += handleMoveCancel;
    }

    protected override void onStateExit()
    {
        controls.MoveStarted -= updateMoveVelocity;
        controls.MoveReleased -= handleMoveCancel;
        active = false;
        body.SetVelocityX(initialVelocityX);
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
        if (!active)
            return;
        float newVelocityX = initialVelocityX + x * accelerationData.MoveVelocityX;
        newVelocityX = clampVelocityX(newVelocityX, accelerationData.MaxVelocityX);
        body.SetVelocityX(newVelocityX);
    }

    // TODO: make a protected virtual method
    private void handleMoveCancel()
    {
        updateMoveVelocity(0);
    }

    protected override void onStateFixedUpdate() { }

    #endregion

    public override void ResetState() { }
}
