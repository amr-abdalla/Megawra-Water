using UnityEngine;
using System.Collections;

public class ErabyAbstractBounceState : ErabyJumpState
{
    [Header("Timing Multipliers")]
    [SerializeField]
    float tapMultiplier = 1.5f;

    [SerializeField]
    ParticleSystem tapParticles = null;

    [SerializeField]
    BounceTapManager tapManager = null;

    #region STATE API
    protected void onAbstractBounceStateEnter()
    {
        onBaseJumpStateEnter();

        if (tapManager.isTapped())
        {
            applyTapMulipier();
        }
        tapManager.OnTap += applyTapMulipier;
        controls.DiveStarted += goToFastFall;
        tapManager.EnableTap();
    }

    protected override void onStateEnter()
    {
        // Debug.Log("Enter bounce");
        onAbstractBounceStateEnter();
    }

    private void applyTapMulipier()
    {
        float newVelocityY = clampVelocityY(body.VelocityY * tapMultiplier);
        body.SetVelocityY(newVelocityY);
        persistentData.initialVelocityY = newVelocityY;
        tapParticles?.Play();
        Debug.Log("Good timing!");
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

    protected override void onStateExit()
    {
        // jumpRiseFrames.Stop();
        tapManager.OnTap -= applyTapMulipier;
        controls.DiveStarted -= goToFastFall;
        tapManager.DisableTap();

        base.onStateExit();
    }

    #endregion
}
