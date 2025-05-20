// using Cinemachine;
using System.Collections;
using UnityEngine;

public abstract class ErabyAbstractFallState : MoveHorizontalAbstractState
{
    #region STATE API
    protected bool groundCheckEnabled = true;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        base.onStateEnter();
        controls.EnableControls();
    }

    protected override void onStateFixedUpdate()
    {
        // Debug.Log("Current Ground: " + body.CurrentGroundTransform?.gameObject.name);
        // Debug.Log("Is Grounded: " + body.IsGrounded);
        if (
            isEnabled
            && body.IsGrounded
            && body.CurrentGroundTransform != null
            && groundCheckEnabled
        )
        {
            onDidEnterGround();
        }

        base.onStateFixedUpdate();
    }

    protected override void onStateUpdate() { }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }

    protected void onGenericFallStateExit() { }

    protected override void onStateExit()
    {
        onGenericFallStateExit();
        base.onStateExit();
    }

    #endregion

    #region PRIVATE

    protected abstract void onDidEnterGround();

    protected void goToLanding<OnGroundedType>()
        where OnGroundedType : State
    {
        // Debug.Log("Go to landing. Current State: " + this.GetType().Name);
        if (null == body.CurrentGroundTransform)
        {
            Debug.LogError("Ground is null");
            return;
        }
        IGroundPlatform ground = body.CurrentGroundTransform.GetComponent<IGroundPlatform>();

        if (null == ground)
        {
            Debug.LogError(
                "Ground is not a platform: " + body.CurrentGroundTransform.gameObject.name
            );
            return;
        }

        dataProvider.landingVelocityY = body.VelocityY;
        dataProvider.landingVelocityX = body.VelocityX;


        body.SetVelocityY(0);
        // controls.DisableControls();

        if (ground.IsBouncy)
        {
            dataProvider.bounceVelocityMultiplier = new Vector2(
                ground.BounceVelocityXMultiplier,
                ground.BounceVelocityYMultiplier
            );
            dataProvider.PlayerJumpHeight = ground.PlayerJumpHeight;
            setState<ErabyLandingState>();
        }
        else
            setState<OnGroundedType>();
    }
    #endregion
}
