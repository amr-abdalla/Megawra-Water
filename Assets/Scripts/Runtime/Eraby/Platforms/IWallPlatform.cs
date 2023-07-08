using System;
using UnityEngine;

public interface IWallPlatform
{
    public float HealthPenalty { get; }

    public float OnBumpTimeDisabled { get; }

    public float BumpMagnitude { get; }
}
