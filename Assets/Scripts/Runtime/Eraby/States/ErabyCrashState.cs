// using Cinemachine;
using UnityEngine;

public class ErabyCrashState : ErabyAbstractBounceState
{
    [SerializeField]
    private float minBounceVelocityY = 0f;

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


        initialVelocityY = persistentData.initialVelocityY / 2;
        if (initialVelocityY > minBounceVelocityY)
            onAbstractBounceStateEnter();
        else
            setState<ErabyWalkState>();
    }

    #endregion

    #region PRIVATE

    #endregion
}
