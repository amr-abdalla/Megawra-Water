using UnityEngine;
using System.Collections;

public class AnimatedTransformFeature : StateFeatureAbstract
{
    [SerializeField]
    private AnimatedTransformData data = null;

    [SerializeField]
    private Transform targetTransform;
    private Vector3 targetPosition => data.TargetPosition;
    private Quaternion targetRotation => data.TargetRotation;
    private Vector3 targetScale => data.TargetScale;
    private AnimationCurve animationCurve => data.AnimationCurve;
    private bool looping => data.Looping;
    private float length => data.Length;
    private bool disablePosition => data.DisablePosition;
    private bool disableRotation => data.DisableRotation;
    private bool disableScale => data.DisableScale;
    private bool resetOnExit => data.ResetOnExit;

    private Coroutine animationCoroutine = null;

    private Vector3 initialPosition = MathConstants.VECTOR_3_ZERO;

    private Quaternion initialRotation = MathConstants.QUATERNION_IDENTITY;

    private Vector3 initialScale = MathConstants.VECTOR_3_ONE;

    protected override void onEnter()
    {
        initialPosition = targetTransform.localPosition;
        initialRotation = targetTransform.localRotation;
        initialScale = targetTransform.localScale;
        // targetScale = Vector3.Scale(initialScale, scaleMultiplier);
        animationCoroutine = StartCoroutine(animationSequence());
        base.onEnter();
    }

    protected override void onExit()
    {
        this.DisposeCoroutine(ref animationCoroutine);
        if (resetOnExit)
        {
            targetTransform.localPosition = initialPosition;
            targetTransform.localRotation = initialRotation;
            targetTransform.localScale = initialScale;
        }
        base.onExit();
    }

    private IEnumerator animationSequence()
    {
        bool firstRun = true;
        bool forward = true;
        float elapsedTime = 0f;

        while (looping || firstRun)
        {
            float val = animationCurve.Evaluate(elapsedTime / length);

            if (!disablePosition)
                targetTransform.localPosition = forward
                    ? Vector3.LerpUnclamped(initialPosition, targetPosition, val)
                    : Vector3.LerpUnclamped(targetPosition, initialPosition, val);

            if (!disableRotation)
                targetTransform.localRotation = forward
                    ? Quaternion.LerpUnclamped(initialRotation, targetRotation, val)
                    : Quaternion.LerpUnclamped(targetRotation, initialRotation, val);

            if (!disableScale)
                targetTransform.localScale = forward
                    ? Vector3.LerpUnclamped(initialScale, targetScale, val)
                    : Vector3.LerpUnclamped(targetScale, initialScale, val);

            elapsedTime += Time.deltaTime;

            if (elapsedTime > length)
            {
                firstRun = false;
                elapsedTime = 0f;
                forward = !forward;
            }

            yield return null;
        }

        this.DisposeCoroutine(ref animationCoroutine);
    }
}
