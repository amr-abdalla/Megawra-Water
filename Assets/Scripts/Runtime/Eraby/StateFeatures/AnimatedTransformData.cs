using UnityEngine;

[CreateAssetMenu(
    fileName = "AnimatedTransformData",
    menuName = "Eraby/StateFeatures/AnimatedTransform",
    order = 2
)]
public class AnimatedTransformData : ScriptableObject
{
    [SerializeField]
    private Vector3 targetPosition = MathConstants.VECTOR_3_ZERO;

    [SerializeField]
    private Quaternion targetRotation = MathConstants.QUATERNION_IDENTITY;

    // [SerializeField]
    private Vector3 scaleMultiplier = MathConstants.VECTOR_3_ONE;

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

    public Vector3 TargetPosition => targetPosition;
    public Quaternion TargetRotation => targetRotation;
    // public Vector3 ScaleMultiplier => scaleMultiplier;
    public Vector3 TargetScale => targetScale;
    public AnimationCurve AnimationCurve => animationCurve;
    public bool Looping => looping;
    public float Length => length;
    public bool DisablePosition => disablePosition;
    public bool DisableRotation => disableRotation;
    public bool DisableScale => disableScale;
    public bool ResetOnExit => resetOnExit;
}
