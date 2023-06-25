using UnityEngine;

public class PersistentErabyData : MonoBehaviour
{
    public float initialVelocityY = 0f;

    public float landingVelocityX = 0f;

    public float bumpDuration = 0f;
    public float bumpMagnitude = 0f;

    public float launchVelocityY = 0f;
    public float launchVelocityX = 0f;

    public HaraPlatformAbstract fallPlatform = null;

    public float maxJumpHeight = 0f;
}
