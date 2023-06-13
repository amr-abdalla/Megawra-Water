using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;

public class BounceTapManager : MonoBehaviour
{
    bool tapped;
    float distance;

    public Action OnTap;

    [SerializeField]
    private Vector2 tapColliderSize = Vector2.zero;

    [SerializeField]
    private Transform erabyTransform = null;

    [SerializeField]
    private Controls controls = null;

    private bool enableTap = false;

    private void Awake()
    {
        controls.BounceStarted += tap;
    }

    public void ResetTap()
    {
        Debug.Log("Reset tap");
        tapped = false;
        distance = 0;
    }

    public void EnableTap()
    {
        enableTap = true;
    }

    public void DisableTap()
    {
        enableTap = false;
    }

    public void tap()
    {
        if (tapped || !enableTap)
            return;

        RaycastHit2D hit = Physics2D.BoxCast(
            erabyTransform.position,
            tapColliderSize,
            0,
            Vector2.zero,
            0,
            1 << LayerMask.NameToLayer("Ground")
        );

        if (hit.collider == null)
            return;

        Debug.Log("Tapped");
        tapped = true;
        distance = hit.distance;
        OnTap?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(erabyTransform.position, tapColliderSize);
    }

    public bool isTapped()
    {
        return tapped;
    }

    public void setDistance(float distance)
    {
        this.distance = distance;
    }

    public float getDistance()
    {
        return distance;
    }
}
