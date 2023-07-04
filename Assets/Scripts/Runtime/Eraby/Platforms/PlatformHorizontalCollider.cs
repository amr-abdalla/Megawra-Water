using UnityEngine;

public class PlatformHorizontalCollider : MonoBehaviour
{
    [SerializeField]
    private Collider2D HorizontalCollider;

    [SerializeField]
    private PlatformCollisionEvents collisionEvents;

    [SerializeField]
    private PlatformHorizontalColliderConfig config;

    public float getHealthPenalty() => config.HealthPenalty;

    public float getTimeDisabled() => config.timeDisabled;

    public float getBumpMagnitude() => config.bumpMagnitude;

    private void Awake()
    {
        if (HorizontalCollider == null)
            HorizontalCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PlayerBumper"))
        {
            collisionEvents.OnBump?.Invoke(other.GetContact(0).normal);
        }
    }

    private void OnDrawGizmos()
    {
        if (HorizontalCollider == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(HorizontalCollider.bounds.center, HorizontalCollider.bounds.size);
    }
}
