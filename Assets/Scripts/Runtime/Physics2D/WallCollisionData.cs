using UnityEngine;

public struct WallCollisionData : IWallDetector2D
{
    public WallCollisionData(Vector2 i_normal, Transform i_wallTransform, Vector2 i_point)
    {
        normal = i_normal;
        CurrentWallTransform = i_wallTransform;
        IsHittingWall = true;
        point = i_point;
    }

    private WallCollisionData(Vector2 i_normal)
    {
        normal = i_normal;
        CurrentWallTransform = null;
        IsHittingWall = false;
        point = Vector2.zero;
    }

    public static WallCollisionData NoCollision => new(Vector2.zero);

    private Vector2 normal;

    public readonly bool IsHittingWall { get; }

    public readonly Vector2 WallNormal => normal;

    public readonly Transform CurrentWallTransform { get; }

    private Vector2 point;

    public readonly Vector2 Point => point;
}
