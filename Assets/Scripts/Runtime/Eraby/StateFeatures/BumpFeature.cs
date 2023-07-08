using UnityEngine;

public class BumpFeature : StateFeatureAbstract
{
    [SerializeField]
    ErabyRBCollisionHandler collisionHandler = null;

    [SerializeField]
    PersistentErabyData persistentData = null;

    protected override void onEnter()
    {
        base.onEnter();
        collisionHandler.OnBump += onBump;
    }

    protected override void onExit()
    {
        base.onExit();
        collisionHandler.OnBump -= onBump;
    }

    private void onBump(float bumpMagnitude, float bumpDuration, Vector2 bumpDirection)
    {
        Debug.Log("Bump");
        persistentData.bumpMagnitude = bumpMagnitude;
        persistentData.bumpDuration = bumpDuration;
        persistentData.bumpDirection = bumpDirection;
        setState<ErabyBumpState>();
    }
}
