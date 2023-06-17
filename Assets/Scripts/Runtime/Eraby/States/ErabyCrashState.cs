// using Cinemachine;
using UnityEngine;

public class ErabyCrashState : ErabyAbstractBounceState
{
    [SerializeField]
    private float minBounceVelocityY = 0f;

    [SerializeField]
    private float velocityXMultiplier = 0.5f;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter crash");

        if (!body.IsGrounded)
        {
            setState<ErabyFallState>();
            return;
        }

        initialVelocityY = persistentData.initialVelocityY / 2;
        if (initialVelocityY > minBounceVelocityY)
        {
            float newVelocityX = -Mathf.Abs(persistentData.landingVelocityX) * velocityXMultiplier;
            newVelocityX = -Mathf.Clamp(
                Mathf.Abs(newVelocityX),
                Mathf.Abs(newVelocityX),
                Mathf.Abs(accelerationData.MaxVelocityX)
            );
            persistentData.launchVelocityX = newVelocityX;
            persistentData.launchVelocityY = initialVelocityY;
            setState<ErabyLaunchState>();
            base.onStateEnter();
        }
        else
            setState<ErabyIdleState>();
    }

    #endregion

    #region PRIVATE

    #endregion
}
