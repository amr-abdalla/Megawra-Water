using System.Collections;
using UnityEngine;

public class ErabyJumpState : MoveHorizontalAbstractState
{
    [Header("Jump Configs")]
    [SerializeField]
    [Range(1f, 30f)]
    protected float maxJumpHeight = 8f;

    Coroutine launchRoutine = null;

    [SerializeField]
    protected float initialVelocityY = 0f;

    #region STATE API
    protected override void onStateInit() { }

    protected void onBaseJumpStateEnter()
    {
        // Debug.Log("Enter jump");



        clampInitialVelocityY();
        if (launchRoutine == null)
            launchRoutine = StartCoroutine(launchSequence());
        else
            Debug.LogError("Launch Routine already running!");
    }

    protected override void onStateEnter()
    {
        onBaseJumpStateEnter();
        controls.DiveStarted += goToFastFall;
    }

    protected override void onStateExit()
    {
        // jumpRiseFrames.Stop();
        controls.DiveStarted -= goToFastFall;
        this.DisposeCoroutine(ref launchRoutine);
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
        checkHeight();
    }

    public override void ResetState()
    {
        base.ResetState();

        StopAllCoroutines();

        onStateExit();
    }
    #endregion

    #region PRIVATE
    private IEnumerator launchSequence()
    {
        yield return this.Wait(0.05f);

        body.SetVelocityY(accelerationData.MaxVelocityY);
        yield return this.Wait(0.07f);
        controls.EnableControls();
        yield return this.Wait(0.13f);

        this.DisposeCoroutine(ref launchRoutine);
    }

    void checkHeight()
    {
        if (false == enabled || launchRoutine != null)
            return;

        if (body.VelocityY <= 0)
        {
            setState<ErabyFallState>();
            return;
        }
    }

    protected void goToFastFall()
    {
        setState<ErabyDiveState>();
    }

    void clampInitialVelocityY()
    {
        // given a desceleration, clamp the initial velocity to reach the desired height
        // u = sqrt(2as)
        float maxInitialVelocityY = Mathf.Sqrt(2 * body.GravityVector.magnitude * maxJumpHeight);
        initialVelocityY = Mathf.Clamp(initialVelocityY, initialVelocityY, maxInitialVelocityY);
    }

    #endregion
}
