using UnityEngine;

[CreateAssetMenu(fileName = "GroundPlatformData", menuName = "eraby/GroundPlatformData", order = 2)]
public class GroundPlatformData : ScriptableObject
{
    [SerializeField]
    private float bounceVelocityYMultiplier = 0f;

    [SerializeField]
    private float bounceVelocityXMultiplier = 0f;

    [SerializeField]
    private float playerJumpHeight = 10f;
    public float BounceVelocityYMultiplier => bounceVelocityYMultiplier;

    public float BounceVelocityXMultiplier => bounceVelocityXMultiplier;

    public float PlayerJumpHeight => playerJumpHeight;
}
