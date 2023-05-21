using UnityEngine;

public class ErabyBounceState : ErabyJumpState
{
    [Header("Extra Configs")]
    [SerializeField]
    InterpolatorsManager interpolatorsManager = null;

    [SerializeField]
    HarrankashPlatformEventDispatcher eventDispatcher = null;

    private HaraPlatformAbstract fallPlatform = null;

    #region STATE API
    protected override void onStateEnter()
    {
        if (!body.IsGrounded)
        {
            setState<ErabyFallState>();
            return;
        }

        HaraPlatformAbstract collidedPlatform = getCollidedPlatformComponent();

        if (collidedPlatform == null)
            return;

        if (collidedPlatform != fallPlatform)
        {
            collidedPlatform.onCollision();
        }

        // sfx suggestion: bouncy jump sound
        // bounceSFX?.Play();
        // jumpSFX?.Play();

        maxJumpHeight = collidedPlatform.MaxJumpHeight;
        accelerationData = collidedPlatform.AccelerationConfig;
        base.onStateEnter();
    }

    public override void ResetState()
    {
        StopAllCoroutines();
        // bounceSFX?.Stop();
        // jumpSFX?.Stop();
        onStateExit();
    }
    #endregion

    #region PRIVATE
    private HaraPlatformAbstract getCollidedPlatformComponent()
    {
        //Debug.LogError("Get component bounce");

        HaraPlatformAbstract collidedPlatform =
            body.CurrentGroundTransform.gameObject.GetComponentInParent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponentInChildren<HaraPlatformAbstract>();

        if (body.CurrentGroundTransform.gameObject.tag == "Finish")
        {
            setState<ErabyCrashState>();
        }
        else if (collidedPlatform == null)
        {
            collidedPlatform = fallPlatform;
            if (collidedPlatform == null)
            {
                Debug.LogError("No hara platform component found! setting state to idle.");
                setState<ErabyCrashState>();
            }
        }

        return collidedPlatform;
    }

    public void SetFallPlatform(HaraPlatformAbstract i_platform)
    {
        fallPlatform = i_platform;
    }
    #endregion
}
