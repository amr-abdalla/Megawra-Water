using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHorizontalCollider : MonoBehaviour
{
    [SerializeField]
    private Collider2D HorizontalCollider;

    [SerializeField]
    private PlatformHorizontalColliderConfig config;

    float getHealthPenalty() => config.HealthPenalty;

    float getTimeDisabled() => config.timeDisabled;

    float getBumpMagnitude() => config.bumpMagnitude;

    private void Awake()
    {
        if (HorizontalCollider == null)
            HorizontalCollider = GetComponent<Collider2D>();
    }

    private void OnDrawGizmos()
    {
        if (HorizontalCollider == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(HorizontalCollider.bounds.center, HorizontalCollider.bounds.size);
    }
}
