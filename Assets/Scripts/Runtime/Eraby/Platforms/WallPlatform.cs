using System;
using UnityEngine;

public class WallPlatform : Platform
{
    [SerializeField]
    private WallPlatformData config;

    public float getHealthPenalty() => config.HealthPenalty;

    public float getTimeDisabled() => config.timeDisabled;

    public float getBumpMagnitude() => config.bumpMagnitude;

    protected override PlatformCollisionData generateCollisionData(Collision2D other)
    {
        PlatformCollisionData ret = new PlatformCollisionData();

        Vector2 dir = other.GetContact(0).normal;
        dir.y = 0f;
        dir.Normalize();
        ret.direction = dir;

        return ret;
    }

}
