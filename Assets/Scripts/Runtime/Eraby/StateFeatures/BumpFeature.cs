using UnityEngine;

public class BumpFeature : StateFeatureAbstract
{
    [SerializeField]
    ErabyRBCollisionHandler collisionHandler = null;

    [SerializeField]
   ErabyStateMachineDataProvider dataProvider = null;

    [SerializeField]
    private PhysicsBody2D physicsBody = null;

    protected override void onUpdate()
    {
        base.onUpdate();
        if (physicsBody.IsHittingWall)
        {
            Transform wallTransfrom = physicsBody.CurrentWallTransform;
            if (null == wallTransfrom)
            {
                Debug.LogError("Platform is hitting a wall but wall transform is null");
                return;
            }

            Debug.Log("Wall Transform: " + wallTransfrom.gameObject.name);
            IWallPlatform wallPlatform = wallTransfrom.gameObject.GetComponent<IWallPlatform>();

            if (null == wallPlatform)
                return;

            Vector2 direction = MathConstants.VECTOR_2_RIGHT;
            direction.x =
                direction.x * physicsBody.transform.position.x > wallTransfrom.position.x ? 1 : -1;

            onBump(wallPlatform.BumpMagnitude, wallPlatform.OnBumpTimeDisabled, direction);
        }
    }

    // protected override void onEnter()
    // {
    //     base.onEnter();
    //     collisionHandler.OnBump += onBump;
    // }

    // protected override void onExit()
    // {
    //     base.onExit();
    //     collisionHandler.OnBump -= onBump;
    // }

    // TODO: Bumping bugs out sometimes when both the player and the platform are moving towards each other. This was partially fixed by giving the player a bigger bump collider.
    private void onBump(float bumpMagnitude, float bumpDuration, Vector2 bumpDirection)
    {
        Debug.Log("Bump");
        dataProvider.bumpMagnitude = bumpMagnitude;
        dataProvider.bumpDuration = bumpDuration;
        dataProvider.bumpDirection = bumpDirection;
        setState<ErabyBumpState>();
    }
}
