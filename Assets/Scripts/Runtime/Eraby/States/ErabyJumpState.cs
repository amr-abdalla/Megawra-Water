using System.Collections;
using UnityEngine;

public class ErabyJumpState : MoveHorizontalAbstractState
{
    [Header("Jump Configs")]
    [SerializeField]
    [Range(1f, 30f)]
    protected float maxJumpHeight = 8f;

    Coroutine launchRoutine = null;

    protected float startJumpY;
    protected float stopJumpY;

    #region STATE API
    protected override void onStateInit() { }

    protected void onBaseJumpStateEnter()
    {
        Debug.Log("jump ||||| " + accelerationData.MaxVelocityY);
        startJumpY = body.transform.position.y;
        stopJumpY = startJumpY + maxJumpHeight;
       // Debug.Log(startJumpY + "  " + stopJumpY);

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
        Debug.Log(body.VelocityY);
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
		{
            return;
        }

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


	private void OnDrawGizmos()
	{
        Color col = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-100, stopJumpY), new Vector3(100, stopJumpY));
        Gizmos.color = col;
	}
}
