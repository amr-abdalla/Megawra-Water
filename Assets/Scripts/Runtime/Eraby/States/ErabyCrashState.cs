// using Cinemachine;
using UnityEngine;

public class ErabyCrashState : ErabyJumpState
{
    [Header("Extra Configs")]
    [SerializeField]
    InterpolatorsManager interpolatorsManager = null;

    [SerializeField]
    HarrankashPlatformEventDispatcher eventDispatcher = null;

    [SerializeField]
    AccelerationConfig2D crashAcceleration = null;
    private HaraPlatformAbstract fallPlatform = null;

    #region STATE API
    protected override void onStateEnter()
    {
        if (!body.IsGrounded)
        {
            setState<ErabyFallState>();
            return;
        }

        // sfx suggestion: bouncy jump sound
        // bounceSFX?.Play();
        // jumpSFX?.Play();

        maxJumpHeight /= 2;
        accelerationData = crashAcceleration;
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

    #endregion
}
