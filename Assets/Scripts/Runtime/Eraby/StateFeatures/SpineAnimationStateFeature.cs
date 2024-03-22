using System.Collections;
using Spine.Unity;
using UnityEngine;

public class SpineAnimationStateFeature : StateFeatureAbstract
{
    [SerializeField]
    private SkeletonAnimation spine = null;

    [SerializeField]
    private string animationName = "Run";

    [SerializeField]
    private bool loop = true;

    [SerializeField]
    private float delay = 0.0f;

    private Coroutine animationCoroutine;

    protected override void OnEnter()
    {
        if (spine)
            animationCoroutine = StartCoroutine(AnimationRoutine());
    }

    private IEnumerator AnimationRoutine()
    {
        yield return new WaitForSeconds(delay);
        if (spine)
            spine.AnimationState.SetAnimation(0, animationName, loop);

        animationCoroutine = null;
    }

    protected override void OnExit()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
    }
}
