using UnityEngine;
using System.Collections;
using Spine.Unity;

public class PlatformBounceState : State
{
    private Coroutine bounceCoroutine;

    [SerializeField]
    private float waitTime = 0.5f;

    [SerializeField]
    private SkeletonAnimation spline = null;
    [SerializeField]
    private string bounceAnimation = "Bounce";
    [SerializeField]
    public string walkAnimation = "Walk";

    [SerializeField]
    private SkeletonDataAsset skeletonDataAsset;
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Bounce State");
        if (spline) spline.AnimationState.SetAnimation(0, bounceAnimation, false);
        bounceCoroutine = StartCoroutine(bounce());
    }

    protected override void onStateExit()
    {
        if (spline) spline.AnimationState.SetAnimation(0, walkAnimation, true);
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
