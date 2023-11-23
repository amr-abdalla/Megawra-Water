using UnityEngine;
using System;

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
    private ErabyControls controls = null;

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
        if (!enableTap)
            Debug.Log("Tap disabled");

        if (tapped)
            Debug.Log("Already tapped");

        if (tapped || !enableTap)
            return;

        Debug.Log("Tapped");
        tapped = true;

        OnTap?.Invoke();
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(erabyTransform.position, tapColliderSize);
    // }

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
