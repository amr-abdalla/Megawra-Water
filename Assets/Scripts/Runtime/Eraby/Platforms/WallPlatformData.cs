using UnityEngine;

[CreateAssetMenu(fileName = "WallPlatformData", menuName = "Eraby/WallPlatformData", order = 0)]
public class WallPlatformData : ScriptableObject
{
    public float HealthPenalty = 0.1f;

    [Header("Front (Trample) Collider")]
    public float timeDisabled = 0.5f;

    [Header("Back (Bump) Collider")]
    public float bumpMagnitude = 10f;
}
