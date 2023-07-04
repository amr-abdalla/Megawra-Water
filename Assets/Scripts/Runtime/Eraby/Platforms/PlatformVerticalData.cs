using UnityEngine;

[CreateAssetMenu(fileName = "PlatformBounceData", menuName = "eraby/PlatformBounceData", order = 2)]
public class PlatformVerticalData : ScriptableObject
{
    [SerializeField]
    private float bounceVelocityYMultiplier = 0f;

    [SerializeField]
    private float bounceVelocityXMultiplier = 0f;
    public float BounceVelocityYMultiplier => bounceVelocityYMultiplier;

    public float BounceVelocityXMultiplier => bounceVelocityXMultiplier;
}
