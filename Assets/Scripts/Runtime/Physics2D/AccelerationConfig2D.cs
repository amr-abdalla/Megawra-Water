using UnityEngine;

[CreateAssetMenu(
    fileName = "AccelerationConfig2D",
    menuName = "com.largelabs.nutrition/AccelerationConfig2D",
    order = 1
)]
public class AccelerationConfig2D : ScriptableObject
{
    [Header("Acceleration")]
    [SerializeField]
    Vector2 acceleration = MathConstants.VECTOR_2_ZERO;

    [SerializeField]
    Vector2 desceleration = MathConstants.VECTOR_2_ZERO;

    [Header("Velocity")]
    [SerializeField]
    Vector2 moveVelocity = MathConstants.VECTOR_2_ZERO;

    [SerializeField]
    Vector2 maxVelocity = MathConstants.VECTOR_2_ZERO;

    #region IAccelerationConfig2D



    public float AccelerationZ
    {
        get { return 0f; }
    }

    public float MaxVelocityX
    {
        get { return maxVelocity.x; }
        set { maxVelocity.x = value; }
    }

    public float MaxVelocityZ
    {
        get { return 0f; }
    }

    public float MoveVelocityX
    {
        get { return moveVelocity.x; }
    }

    public float DecelerationY
    {
        get { return desceleration.y; }
    }

    public float AccelerationY
    {
        get { return acceleration.y; }
    }

    public float MoveVelocityY
    {
        get { return moveVelocity.y; }
    }

    public float MaxVelocityY
    {
        get { return maxVelocity.y; }
        set { maxVelocity.y = value; }
    }

    public float DecelerationX
    {
        get { return desceleration.x; }
    }

    public float AccelerationX
    {
        get { return acceleration.x; }
    }

    #endregion
}
