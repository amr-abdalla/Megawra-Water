using UnityEngine;

[CreateAssetMenu(
    fileName = "ErabyStateMachineDataProvider",
    menuName = "Eraby/ErabyStateMachineDataProvider",
    order = 0
)]
public class ErabyStateMachineDataProvider : AbstractStateMachineDataProvider
{
    /// <summary>
    ///  Base velocity used by states which inherit from <see cref="MoveHorizontalAbstractState" />.
    /// </summary>
    public float initialVelocityX = 0f;

    /// <summary>
    /// Like <see cref="launchVelocityY"/>, used by landing states to calculate the new X velocity.
    /// </summary>
    public float landingVelocityX = 0f;

    /// <summary>
    /// Like <see cref="launchVelocityY"/>, used by landing states to calculate the new Y velocity.
    /// </summary>
    public float landingVelocityY = 0f;

    /// <summary>
    /// Data recieved from a platform when bumped horizontally.
    /// </summary>
    public float bumpDuration = 0f;

    /// <summary>
    /// Data recieved from a platform when bumped horizontally.
    /// </summary>
    public float bumpMagnitude = 0f;

    /// <summary>
    /// Set by landing states, used by launch states to set the player's velocity.
    /// </summary>

    private Vector2 launchVelocity = MathConstants.VECTOR_2_ZERO;

    /// <summary>
    /// Set by landing states, used by launch states to set the player's velocity.
    /// </summary>
    public float launchVelocityY
    {
        get => launchVelocity.y;
        set => launchVelocity.y = value;
    }

    /// <summary>
    /// Set by landing states, used by launch states to set the player's velocity.
    /// </summary>
    public float launchVelocityX
    {
        get => launchVelocity.x;
        set => launchVelocity.x = value;
    }

    /// <summary>
    /// Read from ground platforms. Set by falling states when the player is grounded, read by landing sates to calculate launch velocity.
    /// </summary>
    public Vector2 bounceVelocityMultiplier = MathConstants.VECTOR_2_ONE;

    public IGroundPlatform fallPlatform = null;

    /// <summary>
    /// Calculated by <see cref="ErabyBumFeature"/> when the player bumps into a wall. Sets the direction of the bump used by <see cref="ErabyBumpState"/>.
    /// </summary>
    public Vector2 bumpDirection = Vector2.zero;

    /// <summary>
    /// Used by Gizmos to draw the player's maximum altitude on the current jump.
    /// </summary>
    public float jumpStopHeight = 0f;
}
