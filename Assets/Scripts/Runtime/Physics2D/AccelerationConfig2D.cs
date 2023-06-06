using UnityEngine;

[CreateAssetMenu(
    fileName = "AccelerationConfig2D",
    menuName = "com.largelabs.nutrition/AccelerationConfig2D",
    order = 1
)]
public class AccelerationConfig2D : ScriptableObject
{
    [Header("Velocity")]
    [SerializeField]
    float maxVelX = 0f;

    [SerializeField]
    float minVelX = 0f;

    [SerializeField]
    float moveVelX = 0f;

    [SerializeField]
    float diveVelY = 0f;

    #region IAccelerationConfig2D



    public float AccelerationZ
    {
        get { return 0f; }
    }

    public float MaxVelocityX
    {
        get { return maxVelX; }
        set { maxVelX = value; }
    }

    public float MaxVelocityZ
    {
        get { return 0f; }
    }

    public float MinVelocityX
    {
        get { return minVelX; }
    }

    public float MinVelocityZ
    {
        get { return 0f; }
    }

    public float MoveVelocityX
    {
        get { return moveVelX; }
    }

    public float DiveVelocityY
    {
        get { return diveVelY; }
    }

    #endregion
}
