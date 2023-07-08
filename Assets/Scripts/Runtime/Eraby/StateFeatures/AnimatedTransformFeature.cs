using UnityEngine;
using System.Collections;

public class AnimatedTransformFeature : StateFeatureAbstract
{
    [SerializeField]
    private Transform targetTransform = null;

    [SerializeField]
    private Vector3 targetPosition = MathConstants.VECTOR_3_ZERO;

    [SerializeField]
    private Quaternion targetRotation = MathConstants.QUATERNION_IDENTITY;

    [SerializeField]
    private Vector3 targetScale = MathConstants.VECTOR_3_ONE;

    [SerializeField]
    private AnimationCurve animationCurve = null;

    [SerializeField]
    private bool looping = false;

    [SerializeField]
    private float length = 0f;

    [SerializeField]
    private bool disablePosition = false;

    [SerializeField]
    private bool disableRotation = false;

    [SerializeField]
    private bool disableScale = false;

    [SerializeField]
    private bool resetOnExit = true;

    private Coroutine animationCoroutine = null;

    private Vector3 initialPosition = MathConstants.VECTOR_3_ZERO;

    private Quaternion initialRotation = MathConstants.QUATERNION_IDENTITY;

    private Vector3 initialScale = MathConstants.VECTOR_3_ONE;

    protected override void onEnter()
    {
        initialPosition = targetTransform.localPosition;
        initialRotation = targetTransform.localRotation;
        initialScale = targetTransform.localScale;
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
                elapsedTime = 0;
            }

            yield return null;
        }

        this.DisposeCoroutine(ref animationCoroutine);
    }
}
