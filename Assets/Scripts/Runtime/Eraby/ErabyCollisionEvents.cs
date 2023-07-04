using System.Collections;
using System;
using UnityEngine;

public class ErabyCollisionEvents : MonoBehaviour
{
    public Action<float, float, Vector2> OnBump;
    public Action OnTrample;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision with " + other.gameObject.tag);

        // get direction of collision


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

            Vector2 direction = other.GetContact(0).normal.x * Vector2.right;

            OnBump?.Invoke(magnitude, duration, direction);
        }
    }
}
