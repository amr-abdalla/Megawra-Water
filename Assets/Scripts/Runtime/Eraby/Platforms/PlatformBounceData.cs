using UnityEngine;

[CreateAssetMenu(fileName = "PlatformBounceData", menuName = "eraby/PlatformBounceData", order = 2)]
public class PlatformBounceData : ScriptableObject
{
    [SerializeField]
    private float bounceVelocityYMultiplier = 0f;
    public float BounceVelocityYMultiplier => bounceVelocityYMultiplier;
}
