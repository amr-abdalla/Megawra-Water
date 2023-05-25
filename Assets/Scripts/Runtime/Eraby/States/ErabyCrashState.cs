// using Cinemachine;
using UnityEngine;

public class ErabyCrashState : ErabyAbstractBounceState
{
    AccelerationConfig2D crashAcceleration = null;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter crash");

        if (!body.IsGrounded)
        {
            setState<ErabyFallState>();
            return;
        }

        // sfx suggestion: bouncy jump sound
        // bounceSFX?.Play();
        // jumpSFX?.Play();

        maxJumpHeight /= 2;
        crashAcceleration = Instantiate(accelerationData);
        crashAcceleration.MaxVelocityY /= 2;
        accelerationData = crashAcceleration;

        onAbstractBounceStateEnter();
    }

    #endregion

    #region PRIVATE

    #endregion
}
