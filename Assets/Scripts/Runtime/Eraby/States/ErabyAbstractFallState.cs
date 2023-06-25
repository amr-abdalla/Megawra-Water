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
            goToLanding(body.CurrentGroundTransform.gameObject.tag);
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
    protected virtual void goToLanding(string i_tag)
    {
        return;
    }
    #endregion

    #region UTILITY
    protected HaraPlatformAbstract getCollidedPlatformComponent()
    {
        HaraPlatformAbstract collidedPlatform =
            body.CurrentGroundTransform.gameObject.GetComponentInParent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponentInChildren<HaraPlatformAbstract>();

        return collidedPlatform;
    }
    #endregion
}
