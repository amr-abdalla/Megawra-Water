using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;

[Serializable]
struct Animation
{
    public string name;
    public bool loop;
    public float delay;
}

public class SpineAnimationSequenceStateFeature : StateFeatureAbstract
{
    [SerializeField]
    private SkeletonAnimation spine = null;

    [SerializeField]
    private Animation[] animations = null;

    private Coroutine animationCoroutine;

    protected override void OnEnter()
    {
        if (spine && animations.Length > 0)
            animationCoroutine = StartCoroutine(AnimationRoutine(animations[0]));
    }

    private IEnumerator AnimationRoutine(Animation animation, int index = 0)
    {
        yield return new WaitForSeconds(animation.delay);
        if (spine)
            spine.AnimationState.SetAnimation(0, animation.name, animation.loop);

        yield return new WaitForSeconds(spine.AnimationState.GetCurrent(0).Animation.Duration);
        if (index + 1 < animations.Length)
            animationCoroutine = StartCoroutine(AnimationRoutine(animations[index + 1], index + 1));
        else
            animationCoroutine = null;
    }

    protected override void OnExit()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
    }
}
