using System.Collections;
using System;
using UnityEngine;

public class ErabyCollisionEvents : MonoBehaviour
{
    public Action<float, float> OnBump;
    public Action OnTrample;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trample"))
        {
            OnTrample?.Invoke();
        }
        else if (other.gameObject.CompareTag("Bump"))
        {
            float duration = other.gameObject
                .GetComponent<PlatformHorizontalCollider>()
                .getTimeDisabled();

            float magnitude = other.gameObject
                .GetComponent<PlatformHorizontalCollider>()
                .getBumpMagnitude();

            OnBump?.Invoke(magnitude, duration);
        }
    }
}
