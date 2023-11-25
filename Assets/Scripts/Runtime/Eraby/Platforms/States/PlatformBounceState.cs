using UnityEngine;
using System.Collections;

public class PlatformBounceState : State
{
    private Coroutine bounceCoroutine;

    [SerializeField]
    private float waitTime = 0.5f;

    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Bounce State");
        bounceCoroutine = StartCoroutine(bounce());
    }

    protected override void onStateExit()
    {
        this.DisposeCoroutine(ref bounceCoroutine);
    }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    public override void ResetState() { }

    private void goToIdle()
    {
        setState<PlatformIdleState>();
    }

    private IEnumerator bounce()
    {
        yield return new WaitForSeconds(waitTime);
        goToIdle();
        this.DisposeCoroutine(ref bounceCoroutine);
    }
}
