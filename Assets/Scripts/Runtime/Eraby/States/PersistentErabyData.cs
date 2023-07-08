using UnityEngine;

public class PersistentErabyData : MonoBehaviour
{
    public float initialVelocityY = 0f;

    public float initialVelocityX = 0f;

    public float landingVelocityX = 0f;

    public float bumpDuration = 0f;
    public float bumpMagnitude = 0f;

    public float launchVelocityY = 0f;
    public float launchVelocityX = 0f;

    public Vector2 bounceVelocityMultiplier = MathConstants.VECTOR_2_ONE;

    public IGroundPlatform fallPlatform = null;

    public Vector2 bumpDirection = Vector2.zero;

    public float maxJumpHeight = 0f;
}
