using System.Collections;
using UnityEngine;

public class ErabyJumpState : MoveHorizontalAbstractState
{
    [Header("Jump Configs")]
    [SerializeField]
    [Range(1f, 30f)]
    protected float maxJumpHeight = 8f;

    Coroutine launchRoutine = null;

    float startJumpY = 0f;
    protected float stopJumpY = 0f;

    float desY = 0f;

    #region STATE API
    protected override void onStateInit() { }

    protected void onBaseJumpStateEnter()
    {
        // Debug.Log("Enter jump");

        startJumpY = body.transform.position.y;
        stopJumpY = startJumpY + maxJumpHeight;
        Debug.Log(startJumpY + "  " + stopJumpY);

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

        if (body.transform.position.y >= stopJumpY)
        {
            setState<ErabyFallState>();
            return;
        }

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

    #endregion
}
