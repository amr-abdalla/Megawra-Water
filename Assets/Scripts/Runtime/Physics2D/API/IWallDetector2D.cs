using UnityEngine;

public interface IWallDetector2D
{
    bool IsHittingWall { get; }

    Vector2 WallNormal { get; }

    Transform CurrentWallTransform { get; }
    // Physics2DWallEvent OnWallStatusChanged { get; set; }
}
