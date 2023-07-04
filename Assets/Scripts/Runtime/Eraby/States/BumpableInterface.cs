using UnityEngine;

interface IBumpableState
{
    PersistentErabyData _persistentData { get; set; }
    private void onBump(float bumpMagnitude, float bumpDuration, Vector2 bumpDirection)
    {
        _persistentData.bumpMagnitude = bumpMagnitude;
        _persistentData.bumpDuration = bumpDuration;
        _persistentData.bumpDirection = bumpDirection;
        setState<ErabyBumpState>();
    }
    protected void setState<T>()
        where T : State;
}
