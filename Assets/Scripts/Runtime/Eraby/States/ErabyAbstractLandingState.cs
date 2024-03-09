using UnityEngine;
using System.Collections;

abstract public class ErabyAbstractLandingState : ErabyState
{
    [Header("Timing Multipliers")]
    [SerializeField]
    protected float tapMultiplier = 1.5f;

    [SerializeField]
    protected ParticleSystem tapParticles = null;

    [SerializeField]
    protected BounceTapManager tapManager = null;

    [SerializeField]
    protected PhysicsBody2D body = null;


    [SerializeField]
    protected float landingTime = 0.5f;
    protected Coroutine landingRoutine = null;


    #region STATE API


    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    protected override void onStateEnter()
    {
        body.SetVelocityX(0f);
        controls?.EnableControls();

        tapManager.ResetTap();

        tapManager.EnableTap();
        landingRoutine = StartCoroutine(landingSequence());
    }

    abstract protected void applyTapMulipier();

    abstract protected IEnumerator landingSequence();

    private void onTap()
    {
        tapParticles?.Play();
        Debug.Log("Good timing!");
        applyTapMulipier();
    }

    public override void ResetState()
    {
        StopAllCoroutines();

        onStateExit();
    }
    #endregion

    #region PRIVATE

    protected override void onStateExit()
    {
        if (tapManager.isTapped())
        {
            // onTap();
        }

        this.DisposeCoroutine(ref landingRoutine);
        tapManager.DisableTap();
    }

    protected float clampVelocityX(float velocityX)
    {
        return Mathf.Clamp(
            velocityX,
            -Mathf.Abs(accelerationData.MaxVelocityX),
            Mathf.Abs(accelerationData.MaxVelocityX)
        );
    }

    #endregion
}
