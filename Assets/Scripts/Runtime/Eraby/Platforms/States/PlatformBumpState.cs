using UnityEngine;
using System.Collections;

public class PlatformBumpState : State
{
    [SerializeField]
    private float waitTime = 0.5f;

    private Coroutine bumpCoroutine;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Bump State");
        bumpCoroutine = StartCoroutine(bump());
    }

    protected override void onStateExit()
    {
        this.DisposeCoroutine(ref bumpCoroutine);
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    public override void ResetState() { }

    private void goToIdle()
    {
        setState<PlatformIdleState>();
    }

    private IEnumerator bump()
    {
        yield return new WaitForSeconds(waitTime);
        goToIdle();
        this.DisposeCoroutine(ref bumpCoroutine);
    }
}
