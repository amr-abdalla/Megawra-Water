using UnityEngine;
using System.Collections;

public class ErabyAbstractBounceState : ErabyJumpState
{
    [Header("Extra Configs")]
    [SerializeField]
    InterpolatorsManager interpolatorsManager = null;

    [SerializeField]
    HarrankashPlatformEventDispatcher eventDispatcher = null;

    [Header("Timing Multipliers")]
    [SerializeField]
    float tapMultiplier = 1.5f;

    [SerializeField]
    float timingThreshold = 0.2f;

    [SerializeField]
    ParticleSystem tapParticles = null;

    [SerializeField]
    BounceTapManager tapManager = null;

    Coroutine bounceRoutine = null;

    #region STATE API
    protected void onAbstractBounceStateEnter()
    {
        onBaseJumpStateEnter();
        if (bounceRoutine == null)
            bounceRoutine = StartCoroutine(bounceSequence());
    }

    protected override void onStateEnter()
    {
        // Debug.Log("Enter bounce");
        onAbstractBounceStateEnter();
    }

    private IEnumerator bounceSequence()
    {
        if (tapManager.isTapped())
        {
            applyTapMulipier();
        }
        tapManager.OnTap += applyTapMulipier;
        controls.DiveStarted += goToFastFall;
        yield return null;
        this.DisposeCoroutine(ref bounceRoutine);
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
        this.DisposeCoroutine(ref bounceRoutine);
        tapManager.OnTap -= applyTapMulipier;

        base.onStateExit();
    }

    #endregion
}
