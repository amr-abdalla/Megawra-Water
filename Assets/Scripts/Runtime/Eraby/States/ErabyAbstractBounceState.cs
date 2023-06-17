using UnityEngine;
using System.Collections;

public class ErabyAbstractBounceState : State
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
    protected AccelerationConfig2D accelerationData = null;

    [SerializeField]
    protected PersistentErabyData persistentData = null;

    private bool active = false;

    protected float initialVelocityY = 0f;

    #region STATE API


    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    protected override void onStateEnter()
    {
        active = true;
        if (tapManager.isTapped())
        {
            applyTapMulipier();
        }
        tapManager.OnTap += applyTapMulipier;
        tapManager.EnableTap();
    }

    private void applyTapMulipier()
    {
        if (!active)
            return;

        float newVelocityY = body.VelocityY * tapMultiplier;
        body.SetVelocityY(newVelocityY);
        persistentData.initialVelocityY = newVelocityY;
        tapParticles?.Play();
        Debug.Log("Good timing!");
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
        tapManager.OnTap -= applyTapMulipier;

        tapManager.DisableTap();
    }

    #endregion
}
