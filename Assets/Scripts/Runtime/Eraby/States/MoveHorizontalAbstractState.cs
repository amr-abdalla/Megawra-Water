using UnityEngine;

public abstract class MoveHorizontalAbstractState : State
{
    [SerializeField]
    protected AccelerationConfig2D accelerationData = null;

    [SerializeField]
    protected PhysicsBody2D body = null;

    protected float initialVelocityX = 0f;

    #region PROTECTED

    protected override void onStateEnter()
    {
        initialVelocityX = body.VelocityX;
        controls.MoveStarted += updateMoveVelocity;
        controls.MoveReleased += handleMoveCancel;
    }

    protected override void onStateExit()
    {
        controls.MoveStarted -= updateMoveVelocity;
        controls.MoveReleased -= handleMoveCancel;
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

    void updateMoveVelocity(float x)
    {
        float newVelocityX = initialVelocityX + x * accelerationData.MoveVelocityX;
        newVelocityX = clampVelocityX(newVelocityX, accelerationData.MaxVelocityX);
        body.SetVelocityX(newVelocityX);
    }

    private void handleMoveCancel()
    {
        updateMoveVelocity(0);
    }

    protected override void onStateFixedUpdate() { }

    #endregion

    public override void ResetState() { }
}
