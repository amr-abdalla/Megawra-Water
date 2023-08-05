using UnityEngine;

public class BumpFeature : StateFeatureAbstract
{
    [SerializeField]
    private ErabyStateMachineDataProvider dataProvider = null;

    [SerializeField]
    private PhysicsBody2D physicsBody = null;

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        physicsBody.OnWallStatusChanged += HandleWallStatusChanges;
    }

    protected override void OnExit()
    {
        base.OnExit();
        physicsBody.OnWallStatusChanged -= HandleWallStatusChanges;
    }

    private string SerializeWallDetector(IWallDetector2D wallDetector)
    {
        if (null == wallDetector)
            return "null";

        return "wallDetector: \n\t"
            + wallDetector.CurrentWallTransform?.gameObject.name
            + "\n\t"
            + wallDetector.IsHittingWall
            + "\n\t"
            + wallDetector.WallNormal
            + "\n\t"
            + wallDetector.CurrentWallTransform?.position.x
            + ", "
            + wallDetector.CurrentWallTransform?.position.y
            + ", "
            + wallDetector.CurrentWallTransform?.position.z;
    }

    private void HandleWallStatusChanges(WallCollisionData wallDetector)
    {
        Debug.Log(SerializeWallDetector(wallDetector));

        if (!wallDetector.IsHittingWall || !IsEnabled)
            return;

        Transform wallTransfrom = wallDetector.CurrentWallTransform;
        if (null == wallTransfrom)
        {
            Debug.LogError("Platform is hitting a wall but wall transform is null");
            return;
        }

        IWallPlatform wallPlatform = wallTransfrom.gameObject.GetComponent<IWallPlatform>();

        if (null == wallPlatform)
            return;

        Vector2 direction = wallDetector.WallNormal;

        OnBump(wallPlatform.BumpMagnitude, wallPlatform.OnBumpTimeDisabled, direction);
    }

    // TODO: Bumping bugs out sometimes when both the player and the platform are moving towards each other. This was partially fixed by giving the player a bigger bump collider.
    private void OnBump(float bumpMagnitude, float bumpDuration, Vector2 bumpDirection)
    {
        Debug.Log("Bump");
        physicsBody.toggleCollisionDetection(false);
        dataProvider.bumpMagnitude = bumpMagnitude;
        dataProvider.bumpDuration = bumpDuration;
        dataProvider.bumpDirection = bumpDirection;
        SetState<ErabyBumpState>();
    }
}
