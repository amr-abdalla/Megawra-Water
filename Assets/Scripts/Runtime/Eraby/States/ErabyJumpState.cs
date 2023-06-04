using System.Collections;
using UnityEngine;

public class ErabyJumpState : MoveHorizontalAbstractState
{
    [Header("Jump Configs")]
    [SerializeField]
    [Range(1f, 30f)]
    protected float maxJumpHeight = 8f;

    [SerializeField]
    protected PersistentErabyData persistentData = null;
    Coroutine launchRoutine = null;

    [SerializeField]
    protected float initialVelocityY = 0f;
    float stopJumpY = 0f;
    float startJumpY = 0f;

    #region STATE API
    protected override void onStateInit() { }

    protected void onBaseJumpStateEnter()
    {
        // Debug.Log("Enter jump");



        clampInitialVelocityY();
        startJumpY = body.transform.position.y;
        stopJumpY = body.transform.position.y + maxJumpHeight;
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
        persistentData.initialVelocityY = initialVelocityY;
        controls.DiveStarted -= goToFastFall;
        this.DisposeCoroutine(ref launchRoutine);
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
        // Debug.Log(body.VelocityY);
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

        body.SetVelocityY(initialVelocityY);
        yield return this.Wait(0.07f);
        controls.EnableControls();
        yield return this.Wait(0.13f);

        this.DisposeCoroutine(ref launchRoutine);
    }

    void checkHeight()
    {
        if (false == enabled || launchRoutine != null)
        {
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

    void clampInitialVelocityY()
    {
        initialVelocityY = clampVelocityY(initialVelocityY);
    }

    protected float clampVelocityY(float velocityY)
    {
        // given a desceleration, clamp the initial velocity to reach the desired height
        // u = sqrt(2as)
        float maxInitialVelocityY = Mathf.Sqrt(
            2
                * body.GravityVector.magnitude
                * (maxJumpHeight - (body.transform.position.y - startJumpY))
        );
        return Mathf.Clamp(velocityY, velocityY, maxInitialVelocityY);
    }

    #endregion



    private void OnDrawGizmos()
    {
        Color col = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-100, stopJumpY), new Vector3(100, stopJumpY));
        Gizmos.color = col;
    }
}
