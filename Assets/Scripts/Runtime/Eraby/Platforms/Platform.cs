using System;
using UnityEngine;

public struct PlatformCollisionData
{
    public Vector2 direction;
    public string tag;
}

public abstract class Platform : MonoBehaviourBase
{
    [SerializeField]
    private Collider2D platformCollider = null;

    [SerializeField]
    private string compatibleTag = null;
    public Action<PlatformCollisionData> onCollision = null;

    protected override void Awake()
    {
        base.Awake();

        if (platformCollider == null)
            platformCollider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool canDispatchCollision = true;
        if (!string.IsNullOrEmpty(compatibleTag))
        {
            canDispatchCollision = other.gameObject.CompareTag(compatibleTag);
        }

        if (canDispatchCollision)
        {
            PlatformCollisionData collisionParams = generateCollisionData(other);
            onCollision?.Invoke(collisionParams);
            onDidHandleCollision(other, collisionParams);
        }
    }

    protected abstract PlatformCollisionData generateCollisionData(Collision2D other);

    protected virtual void onDidHandleCollision(
        Collision2D i_other,
        PlatformCollisionData i_collisionParams
    ) { }

    public void EnableCollider(bool i_enable)
    {
        if (null == platformCollider)
            return;
        platformCollider.enabled = i_enable;
    }

    private void OnDrawGizmos()
    {
        if (platformCollider == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(platformCollider.bounds.center, platformCollider.bounds.size);
    }
}
