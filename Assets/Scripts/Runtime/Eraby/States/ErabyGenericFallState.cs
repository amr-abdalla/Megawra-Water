// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabyGenericFallState : MoveHorizontalAbstractState
{
    [SerializeField]
    protected float timeBeforeBounce = 0;

    [SerializeField]
    protected PersistentErabyData persistentData = null;

    [Header("Extra Configs")]
    [SerializeField]
    protected ErabyBounceState bounceState = null;

    [SerializeField]
    protected BounceTapManager tapManager = null;
    protected Coroutine landingRoutine = null;

    #region STATE API
    protected override void onStateInit() { }

    protected void onGenericFallStateEnter()
    {
        tapManager.ResetTap();
        tapManager.EnableTap();
        base.onStateEnter();
        controls.EnableControls();
    }

    protected override void onStateUpdate()
    {
        if (body.IsGrounded && body.CurrentGroundTransform != null)
        {
            if (landingRoutine == null)
                landingRoutine = StartCoroutine(
                    landingSequence(body.CurrentGroundTransform.gameObject.tag)
                );
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
        this.DisposeCoroutine(ref landingRoutine);
    }

    protected override void onStateExit()
    {
        onGenericFallStateExit();
        base.onStateExit();
    }

    #endregion

    #region PRIVATE
    protected virtual IEnumerator landingSequence(string i_tag)
    {
        yield return null;

        this.DisposeCoroutine(ref landingRoutine);
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
