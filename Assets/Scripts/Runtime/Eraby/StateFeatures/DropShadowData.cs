using UnityEngine;

[CreateAssetMenu(
    fileName = "DropShadowData",
    menuName = "Eraby/StateFeatures/DropShadow",
    order = 2
)]
public class DropShadowData : ScriptableObject
{
    [SerializeField]
    private float shadowHeight = 0.1f;

    [SerializeField]
    private float initialShadowScale = 1f;

    [SerializeField]
    private float maxShadowScale = 1.5f;

    [SerializeField]
    private AnimationCurve shadowScaleCurve = null;

    [SerializeField]
    private float maxPlayerHeight = 1f;

    [SerializeField]
    private float minPlayerHeight = 0.5f;

    public float ShadowHeight => shadowHeight;
    public float InitialShadowScale => initialShadowScale;
    public float MaxShadowScale => maxShadowScale;
    public AnimationCurve ShadowScaleCurve => shadowScaleCurve;
    public float MaxPlayerHeight => maxPlayerHeight;
    public float MinPlayerHeight => minPlayerHeight;
}
