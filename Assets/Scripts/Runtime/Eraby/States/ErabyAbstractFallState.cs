// using Cinemachine;
using System.Collections;
using UnityEngine;

abstract public class ErabyAbstractFallState : MoveHorizontalAbstractState
{
    [SerializeField]
    protected float timeBeforeBounce = 0;

    [SerializeField]
    protected PersistentErabyData persistentData = null;

    [Header("Extra Configs")]
    [SerializeField]
    protected BounceTapManager tapManager = null;

    #region STATE API
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {

        tapManager.ResetTap();
        tapManager.EnableTap();
        base.onStateEnter();
        controls.EnableControls();
    }

    protected override void onStateUpdate()
    {
        if (isEnabled && body.IsGrounded && body.CurrentGroundTransform != null)
        {
            onDidEnterGround();
        }
    }



    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }

    protected void onGenericFallStateExit()
    {
        tapManager.DisableTap();
    }

    protected override void onStateExit()
    {
        onGenericFallStateExit();
        base.onStateExit();
    }

    #endregion

    #region PRIVATE

    protected abstract void onDidEnterGround();

    protected void goToLanding<OnGroundedType>() where OnGroundedType : State
    {
        if (null == body.CurrentGroundTransform) return;
        GroundPlatform ground = body.CurrentGroundTransform.GetComponent<GroundPlatform>();

        if (null == ground) return;

        Debug.Log("Landing sequence");
        persistentData.landingVelocityX = body.VelocityX;

        body.SetVelocityX(0);
        body.SetVelocityY(0);
        controls.DisableControls();

        if (ground.IsBouncy)
        {
            persistentData.bounceVelocityMultiplier = new Vector2(ground.BounceVelocityXMultiplier, ground.BounceVelocityYMultiplier);
            setState<ErabyLandingState>();
        }
        else
            setState<OnGroundedType>();
    }
    #endregion

}
