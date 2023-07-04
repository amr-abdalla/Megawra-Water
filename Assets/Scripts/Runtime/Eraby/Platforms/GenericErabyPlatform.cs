using UnityEngine;
using System.Collections;

public class GenericErabyPlatform : HaraPlatformAbstract
{
    [SerializeField]
    PlatformCollisionEvents collisionEvents;

    public override void onCollision()
    {
        collisionEvents.OnBounce?.Invoke();
    }
}
