// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabyGenericFallState : MoveHorizontalAbstractState
{
    [SerializeField]
    protected float timeBeforeBounce = 0.5f;

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

    float startTime;

    protected void onGenericFallStateEnter()
    {
        startTime = Time.time;

        tapManager.ResetTap();

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
    #endregion

    #region PRIVATE
    protected virtual IEnumerator landingSequence(string i_tag)
    {
        // Debug.Log("Landing sequence");

        yield return null;

        this.DisposeCoroutine(ref landingRoutine);
    }
    #endregion

    #region UTILITY
    protected HaraPlatformAbstract getCollidedPlatformComponent()
    {
        //Debug.LogError("Get component fall");
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
